using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using Caliburn.Micro;
using Serilog;
using WpfAppCore.Services;
using WpfAppCore.ViewModels;

namespace WpfAppCore;

public class Bootstrapper : BootstrapperBase
{
    private static readonly ILogger _log = Log.ForContext<Bootstrapper>();

    private const string LOG_FORMAT =
        "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}][{Level:u4}]<{ThreadName}:{ThreadId}> {SourceContext} - {Message:lj}{NewLine}{Exception}";

    private static readonly string _dataFolder =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TestApp");

    private static readonly string _settingsFilename = Path.Combine(_dataFolder, "settings.json");
    private static readonly string _logFilename = Path.Combine(_dataFolder, "out.log");
    private static readonly SimpleContainer _container = new();

    public Bootstrapper()
    {
        Thread.CurrentThread.Name = "MainThread";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .WriteTo.Async(l => l.Console(outputTemplate: LOG_FORMAT))
            .WriteTo.Async(l => l.File(
                _logFilename,
                outputTemplate: LOG_FORMAT,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 10
            ))
            .CreateLogger();

        Initialize();
    }

    protected override void Configure()
    {
        _container
            .Singleton<IWindowManager, WindowManager>()
            .Singleton<SettingsManager>()
            ;

        GetType().Assembly.GetTypes()
            .Where(x => x.IsClass)
            .Where(y => y.Name.EndsWith("ViewModel")).ToList()
            .ForEach(z => _container.RegisterPerRequest(z, z.ToString(), z));

        _log.Information("Application Configured");
    }

    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        SettingsManager.LoadFromJsonFile(_settingsFilename);
        await DisplayRootViewForAsync<ShellViewModel>();
    }

    protected override void OnExit(object sender, EventArgs e)
    {
        SettingsManager.SaveToJsonFile(_settingsFilename);
        Log.CloseAndFlush();
    }

    protected override object GetInstance(Type service, string key)
    {
        return _container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
        _container.BuildUp(instance);
    }
}
