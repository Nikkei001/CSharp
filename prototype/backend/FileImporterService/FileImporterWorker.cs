using Cronos;
using Npgsql;
using CsvHelper;
using System.Globalization;
using ExcelDataReader;
using System.Data;
using System.Text;

namespace FileImporterService;

public class FileImporterWorker : BackgroundService
{
    private readonly ILogger<FileImporterWorker> _logger;
    private readonly FileImportSettings _settings;
    private readonly IServiceProvider _serviceProvider;
    private readonly CronExpression _cronExpression;
    private readonly string _connectionString;
    private Timer? _timer;

    public FileImporterWorker(ILogger<FileImporterWorker> logger, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _settings = configuration.GetSection("FileImportSettings").Get<FileImportSettings>() ?? new FileImportSettings();
        _cronExpression = CronExpression.Parse(_settings.Schedule);
        _connectionString = configuration.GetConnectionString("PostgresConnection") ?? throw new InvalidOperationException("PostgresConnection not found");
        
        // 注册编码提供程序以支持Excel文件读取
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("File Importer Worker running at: {time}", DateTimeOffset.Now);
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); // Check every minute
        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        var now = DateTime.UtcNow;
        var next = _cronExpression.GetNextOccurrence(now);

        // 添加调试信息
        _logger.LogInformation("Current time: {CurrentTime}, Next scheduled time: {NextTime}", 
            now.ToString("yyyy-MM-dd HH:mm:ss"), 
            next?.ToString("yyyy-MM-dd HH:mm:ss") ?? "None");

        if (next.HasValue)
        {
            var delay = next.Value - now;
            _logger.LogInformation("Time until next execution: {Delay} minutes", delay.TotalMinutes);
            
            // 修改触发条件：允许60秒的时间窗口
            if (delay.TotalSeconds <= 60 && delay.TotalSeconds >= 0)
            {
                _logger.LogInformation("Scheduled task is running.");
                await ProcessFiles();
            }
        }
    }

    private async Task ProcessFiles()
    {
        _logger.LogInformation("Checking for files in {SourceDirectory}...", _settings.SourceDirectory);

        try
        {
            // 检查源目录是否存在
            if (!Directory.Exists(_settings.SourceDirectory))
            {
                _logger.LogError("Source directory does not exist: {SourceDirectory}", _settings.SourceDirectory);
                return;
            }

            // 获取所有子文件夹
            var subDirectories = Directory.GetDirectories(_settings.SourceDirectory);

            if (!subDirectories.Any())
            {
                _logger.LogInformation("No subdirectories found in {SourceDirectory}.", _settings.SourceDirectory);
                return;
            }

            _logger.LogInformation("Found {Count} subdirectories to process.", subDirectories.Length);

            // 遍历每个子文件夹
            foreach (var subDirectory in subDirectories)
            {
                var subDirName = Path.GetFileName(subDirectory);
                _logger.LogInformation("Processing subdirectory: {SubDirectory}", subDirName);

                try
                {
                    // 获取子文件夹中的所有支持的文件
                    var files = Directory.GetFiles(subDirectory, "*.*")
                        .Where(f => IsSupportedFileType(f))
                        .ToArray();

                    if (!files.Any())
                    {
                        _logger.LogInformation("No supported files found in subdirectory: {SubDirectory}", subDirName);
                        continue;
                    }

                    _logger.LogInformation("Found {Count} supported files in subdirectory: {SubDirectory}", files.Length, subDirName);

                    // 遍历子文件夹中的每个文件
                    foreach (var file in files)
                    {
                        try
                        {
                            var fileName = Path.GetFileName(file);
                            var fileInfo = new FileInfo(file);
                            
                            _logger.LogInformation("Processing file: {FileName} (Size: {Size} bytes) in {SubDirectory}", 
                                fileName, fileInfo.Length, subDirName);

                            // 读取文件并导入数据库
                            await ImportFileToDatabase(file, fileName, subDirName);
                            
                            _logger.LogInformation("✓ Successfully processed and imported file: {FileName} in {SubDirectory}", 
                                fileName, subDirName);
                        }
                        catch (Exception ex)
                        {
                            var fileName = Path.GetFileName(file);
                            _logger.LogError(ex, "✗ Failed to process file: {FileName} in {SubDirectory}. Error: {ErrorMessage}", 
                                fileName, subDirName, ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing subdirectory: {SubDirectory}. Error: {ErrorMessage}", 
                        subDirName, ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing files: {ErrorMessage}", ex.Message);
        }

        _logger.LogInformation("File processing finished.");
    }

    private bool IsSupportedFileType(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return extension is ".csv" or ".xls" or ".xlsx" or ".txt";
    }

    private async Task ImportFileToDatabase(string filePath, string fileName, string subDirName)
    {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        DataTable dataTable;

        // 根据文件类型读取数据
        switch (extension)
        {
            case ".csv":
            case ".txt":
                dataTable = ReadCsvFile(filePath);
                break;
            case ".xls":
            case ".xlsx":
                dataTable = ReadExcelFile(filePath);
                break;
            default:
                throw new NotSupportedException($"File type {extension} is not supported");
        }

        if (dataTable.Rows.Count == 0)
        {
            _logger.LogWarning("File {FileName} is empty or has no data rows", fileName);
            return;
        }

        // 创建表名
        var tableName = CreateTableName(fileName);
        
        // 导入到数据库
        await ImportDataTableToPostgres(dataTable, tableName, fileName);
    }

    private DataTable ReadCsvFile(string filePath)
    {
        var dataTable = new DataTable();
        
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        // 读取表头
        csv.Read();
        csv.ReadHeader();
        var headers = csv.HeaderRecord;
        
        if (headers != null)
        {
            foreach (var header in headers)
            {
                dataTable.Columns.Add(SanitizeColumnName(header), typeof(string));
            }
        }

        // 读取数据行
        while (csv.Read())
        {
            var row = dataTable.NewRow();
            for (int i = 0; i < headers?.Length; i++)
            {
                row[i] = csv.GetField(i) ?? "";
            }
            dataTable.Rows.Add(row);
        }

        return dataTable;
    }

    private DataTable ReadExcelFile(string filePath)
    {
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        using var reader = ExcelReaderFactory.CreateReader(stream);
        
        var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration()
            {
                UseHeaderRow = true
            }
        });

        var dataTable = dataSet.Tables[0];
        
        // 将所有列转换为string类型
        var newTable = new DataTable();
        foreach (DataColumn column in dataTable.Columns)
        {
            newTable.Columns.Add(SanitizeColumnName(column.ColumnName), typeof(string));
        }

        foreach (DataRow row in dataTable.Rows)
        {
            var newRow = newTable.NewRow();
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                newRow[i] = row[i]?.ToString() ?? "";
            }
            newTable.Rows.Add(newRow);
        }

        return newTable;
    }

    private string SanitizeColumnName(string columnName)
    {
        // 清理列名，确保符合PostgreSQL命名规范
        if (string.IsNullOrWhiteSpace(columnName))
            return "column_unnamed";
            
        var sanitized = columnName.Trim()
            .Replace(" ", "_")
            .Replace("-", "_")
            .Replace(".", "_")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("/", "_")
            .Replace("\\", "_");
            
        // 确保以字母开头
        if (!char.IsLetter(sanitized[0]))
            sanitized = "col_" + sanitized;
            
        return sanitized.ToLowerInvariant();
    }

    private string CreateTableName(string fileName)
    {
        var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var sanitizedName = SanitizeColumnName(nameWithoutExtension);
        return $"temp_{sanitizedName}";
    }

    private async Task ImportDataTableToPostgres(DataTable dataTable, string tableName, string fileName)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        try
        {
            // 删除已存在的表
            var dropTableSql = $"DROP TABLE IF EXISTS {tableName}";
            using var dropCommand = new NpgsqlCommand(dropTableSql, connection);
            await dropCommand.ExecuteNonQueryAsync();
            
            _logger.LogInformation("Dropped existing table {TableName} if it existed", tableName);

            // 创建表结构
            var createTableSql = GenerateCreateTableSql(tableName, dataTable);
            using var createCommand = new NpgsqlCommand(createTableSql, connection);
            await createCommand.ExecuteNonQueryAsync();
            
            _logger.LogInformation("Created table {TableName} with {ColumnCount} columns", tableName, dataTable.Columns.Count);

            // 批量插入数据
            var insertedRows = 0;
            using var writer = connection.BeginBinaryImport($"COPY {tableName} FROM STDIN (FORMAT BINARY)");
            
            foreach (DataRow row in dataTable.Rows)
            {
                writer.StartRow();
                foreach (var item in row.ItemArray)
                {
                    writer.Write(item?.ToString() ?? "", NpgsqlTypes.NpgsqlDbType.Text);
                }
                insertedRows++;
            }
            
            await writer.CompleteAsync();
            
            _logger.LogInformation("Successfully imported {RowCount} rows from file {FileName} to table {TableName}", 
                insertedRows, fileName, tableName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to import file {FileName} to table {TableName}: {ErrorMessage}", 
                fileName, tableName, ex.Message);
            throw;
        }
    }

    private string GenerateCreateTableSql(string tableName, DataTable dataTable)
    {
        var columns = dataTable.Columns.Cast<DataColumn>()
            .Select(column => $"{column.ColumnName} TEXT")
            .ToArray();
            
        return $"CREATE TABLE {tableName} ({string.Join(", ", columns)})";
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("File Importer Worker is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        await base.StopAsync(stoppingToken);
    }
}