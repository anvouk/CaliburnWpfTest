using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using AdonisUI;
using Caliburn.Micro;
using Serilog;

namespace WpfAppCore.Services;

[Serializable]
public class SettingsManager
{
    private static readonly ILogger _log = Log.ForContext<SettingsManager>();

    [JsonPropertyName("currentTheme")]
    public string CurrentTheme { get; set; } = "Light";

    public void ChangeTheme(string theme)
    {
        if (theme == CurrentTheme) return;
        CurrentTheme = theme;
        Uri? th = theme == "Light" ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme;
        _log.Information($"Changing theme to: {theme}");
        ResourceLocator.SetColorScheme(Application.Current.Resources, th);
    }

    public static void SaveToJsonFile(string filename)
    {
        try
        {
            SettingsManager? settingsManager = IoC.Get<SettingsManager>();
            JsonSerializerOptions serializerOptions = new()
            {
                WriteIndented = true
            };
            string serializedRes = JsonSerializer.Serialize(settingsManager, serializerOptions);
            File.WriteAllText(filename, serializedRes);
        }
        catch (Exception e)
        {
            _log.Error(e, "Failed saving app config");
        }
    }

    public static void LoadFromJsonFile(string filename)
    {
        try
        {
            SettingsManager? settingsManager = IoC.Get<SettingsManager>();
            SettingsManager? loadedSettings = JsonSerializer.Deserialize<SettingsManager>(File.ReadAllText(filename));
            settingsManager.ChangeTheme(loadedSettings.CurrentTheme);
        }
        catch (Exception e)
        {
            _log.Warning(e, "Failed loading app config. Using default config");
            LoadDefaultSettings();
        }
    }

    public static void LoadDefaultSettings()
    {
        SettingsManager? settingsManager = IoC.Get<SettingsManager>();
        settingsManager.ChangeTheme("Light");
    }
}
