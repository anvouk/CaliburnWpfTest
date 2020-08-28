using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace WpfAppCore.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IWindowManager _windowManager;
        
        public ShellViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public string? SelectedComboBox { get; set; } = "aa";
        public List<string> ComboBox { get; } = new List<string>
        {
            "aa", "bb"
        };

        public void ShowList()
        {
            _windowManager.ShowWindow(IoC.Get<ListViewModel>());
        }
        
        public void MenuItem_New()
        {
            Console.WriteLine("Hi");
            Console.WriteLine(SelectedComboBox ?? "nada");
        }

        public void MenuItem_Settings()
        {
            _windowManager.ShowDialog(IoC.Get<SettingsViewModel>());
        }
    }
}
