using Avalonia;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace GetStartedApp;

class Program
{
    public static IHost? AppHost;

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
        var builder = new ConfigurationBuilder();
        BuildConfig(builder);
        IConfiguration config = builder.Build();

        string? logFileName = config.GetValue<string>("LogFileName");

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .WriteTo.Console()
            .WriteTo.File(logFileName ?? "app.log")
            .CreateLogger();

        logger.Information("Application is started");

        var serviceBuilder = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddTransient<ITestDependency, TestDependency>();
                services.AddTransient<MainWindow>();
            })
            .UseSerilog(logger);
        var host = serviceBuilder.Build();
        AppHost = host;

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
    static void BuildConfig(IConfigurationBuilder configurationBuilder)
    {
        string? exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        configurationBuilder.SetBasePath(exeDir)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    }
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
