using FileImporterService;

var builder = Host.CreateApplicationBuilder(args);

// 注册配置：将 appsettings.json 中的 FileImportSettings 部分绑定到 FileImportSettings 类
builder.Services.Configure<FileImportSettings>(builder.Configuration.GetSection("FileImportSettings"));

// 注册我们的后台服务：将 FileImporterWorker 添加为托管服务
builder.Services.AddHostedService<FileImporterWorker>();

// 添加 Windows 服务支持
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "FileImporterService";
});

var host = builder.Build();
host.Run();
