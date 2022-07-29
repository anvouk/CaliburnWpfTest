using System;
using System.IO;
using System.Windows;
using AdonisUI;
using Caliburn.Micro;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WpfAppCore.Services;

[Serializable]
public class SettingsManager
{
    [JsonPropertyName("currentTheme")]
    public string CurrentTheme { get; set; } = "Light";

    public void ChangeTheme(string theme)
    {
        if (theme == CurrentTheme) return;
        CurrentTheme = theme;
        var th = theme == "Light" ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme;
        ResourceLocator.SetColorScheme(Application.Current.Resources, th);
    }

    public static void SaveToJsonFile(string filename)
    {
        try
        {
            var settingsManager = IoC.Get<SettingsManager>();
            var serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string serializedRes = JsonSerializer.Serialize(settingsManager, serializerOptions);
            File.WriteAllText(filename, serializedRes);
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
            var settingsManager = IoC.Get<SettingsManager>();
            var loadedSettings = JsonSerializer.Deserialize<SettingsManager>(File.ReadAllText(filename));
            MessageBox.Show(loadedSettings.CurrentTheme, "Theme");
            settingsManager.ChangeTheme(loadedSettings.CurrentTheme);
        }
        catch (Exception e)
        {
            LoadDefaultSettings();
        }
    }

    public static void LoadDefaultSettings()
    {
        var settingsManager = IoC.Get<SettingsManager>();
        settingsManager.ChangeTheme("Light");
    }
}
