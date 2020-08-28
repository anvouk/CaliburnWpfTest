using System;
using System.IO;
using System.Windows;
using AdonisUI;
using Caliburn.Micro;
using Newtonsoft.Json;

namespace WpfAppCore.Services
{
    [Serializable]
    public class SettingsManager
    {
        [JsonProperty(PropertyName = "currentTheme")]
        public string CurrentTheme { get; private set; } = "Light";

        public void ChangeTheme(string theme)
        {
            if (theme == CurrentTheme) return;
            CurrentTheme = theme;
            Uri th = theme == "Light" ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme;
            ResourceLocator.SetColorScheme(Application.Current.Resources, th);
        }

        public static void SaveToJsonFile(string filename)
        {
            try
            {
                SettingsManager settingsManager = IoC.Get<SettingsManager>();
                using StreamWriter file = File.CreateText(filename);
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, settingsManager);
            }
            catch (Exception e)
            {
                // TODO: handle
                Console.WriteLine(e);
            }
        }

        public static void LoadFromJsonFile(string filename)
        {
            try
            {
                SettingsManager settingsManager = IoC.Get<SettingsManager>();
                SettingsManager loadedSettings = JsonConvert.DeserializeObject<SettingsManager>(File.ReadAllText(filename));
                settingsManager.ChangeTheme(loadedSettings.CurrentTheme);
            }
            catch (Exception e)
            {
                LoadDefaultSettings();
            }
        }

        public static void LoadDefaultSettings()
        {
            SettingsManager settingsManager = IoC.Get<SettingsManager>();
            settingsManager.ChangeTheme("Light");
        }
    }
}
