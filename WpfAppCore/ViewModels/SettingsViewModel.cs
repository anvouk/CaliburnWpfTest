using System;
using System.Collections.Generic;
using System.Windows;
using AdonisUI;
using Caliburn.Micro;
using WpfAppCore.Services;

namespace WpfAppCore.ViewModels
{
    public class SettingsViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly SettingsManager _settingsManager;

        public SettingsViewModel(SettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            SelectedThemeComboBox = settingsManager.CurrentTheme;
        }

        public string? SelectedThemeComboBox { get; set; }
        public List<string> ThemeComboBox { get; } = new List<string>
        {
            "Light", "Dark"
        };

        public void ChangeTheme(string selectedTheme)
        {
            _settingsManager.ChangeTheme(selectedTheme);
        }
    }
}
