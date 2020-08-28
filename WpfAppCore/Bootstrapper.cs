using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Serilog;
using WpfAppCore.Services;
using WpfAppCore.ViewModels;

namespace WpfAppCore
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;

        private static readonly string DataFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TestApp");

        private static readonly string SettingsFilename = Path.Combine(DataFolder, "settings.json");
        private static readonly string LogFilename = Path.Combine(DataFolder, "out.log");
        private const string LogFormat =
            "[{Timestamp:yyyy-MM-dd HH:mm:ss}][{Level:u4}] {Message:lj}{NewLine}{Exception}";

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                #if DEBUG
                .WriteTo.Console(outputTemplate: LogFormat)
                #else
                .WriteTo.File(LogFilename, outputTemplate: LogFormat)
                #endif
                .CreateLogger();

            _container = new SimpleContainer();

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<SettingsManager>()
                ;

            GetType().Assembly.GetTypes()
                .Where(x => x.IsClass)
                .Where(y => y.Name.EndsWith("ViewModel")).ToList()
                .ForEach(z => _container.RegisterPerRequest(z, z.ToString(), z));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            SettingsManager.LoadFromJsonFile(SettingsFilename);
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            SettingsManager.SaveToJsonFile(SettingsFilename);
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
}
