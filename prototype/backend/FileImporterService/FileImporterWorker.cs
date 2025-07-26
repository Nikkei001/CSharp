using Cronos;

namespace FileImporterService;

public class FileImporterWorker : BackgroundService
{
    private readonly ILogger<FileImporterWorker> _logger;
    private readonly FileImportSettings _settings;
    private readonly IServiceProvider _serviceProvider;
    private readonly CronExpression _cronExpression;
    private Timer? _timer;

    public FileImporterWorker(ILogger<FileImporterWorker> logger, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _settings = configuration.GetSection("FileImportSettings").Get<FileImportSettings>() ?? new FileImportSettings();
        _cronExpression = CronExpression.Parse(_settings.Schedule);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("File Importer Worker running at: {time}", DateTimeOffset.Now);
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); // Check every minute
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        var now = DateTime.UtcNow;
        var next = _cronExpression.GetNextOccurrence(now);

        if (next.HasValue)
        {
            var delay = next.Value - now;
            if (delay.TotalMilliseconds <= 1)   // Check if the time has passed
            {
                _logger.LogInformation("Scheduled task is running.");
                ProcessFiles();
            }
        }
    }

    private void ProcessFiles()
    {
        _logger.LogInformation("Checking for files in {SourceDirectory}...", _settings.SourceDirectory);

        try
        {
            var files = Directory.GetFiles(_settings.SourceDirectory);

            if (!files.Any())
            {
                _logger.LogInformation("No files to process.");
                return;
            }

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.GetFileName(file);
                    var destFile = Path.Combine(_settings.ProcessedDirectory, fileName);
                    File.Move(file, destFile);
                    _logger.LogInformation("Processed file {FileName} successfully.", fileName);
                }
                catch (Exception ex)
                {
                    var fileName = Path.GetFileName(file);
                    _logger.LogError(ex, "Error processing file {FileName}. Moving to error directory.", fileName);
                    var errorFile = Path.Combine(_settings.ErrorDirectory, fileName);
                    File.Move(file, errorFile);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing files.");
        }

        _logger.LogInformation("File processing finished.");
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("File Importer Worker is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        await base.StopAsync(stoppingToken);
    }
}