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

        public async void ShowList()
        {
            await _windowManager.ShowWindowAsync(IoC.Get<ListViewModel>());
        }

        public void MenuItem_New()
        {
            Console.WriteLine("Hi");
            Console.WriteLine(SelectedComboBox ?? "nada");
        }

        public async void MenuItem_Settings()
        {
            await _windowManager.ShowWindowAsync(IoC.Get<SettingsViewModel>());
        }
    }
}
