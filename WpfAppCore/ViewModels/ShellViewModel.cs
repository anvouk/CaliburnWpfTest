using System.Collections.Generic;
using Caliburn.Micro;
using Serilog;

namespace WpfAppCore.ViewModels;

public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
{
    private static readonly ILogger _log = Log.ForContext<ShellViewModel>();

    private readonly IWindowManager _windowManager;

    public ShellViewModel(IWindowManager windowManager)
    {
        _windowManager = windowManager;
    }

    public string? SelectedComboBox { get; set; } = "aa";

    public List<string> ComboBox { get; } = new()
    {
        "aa", "bb"
    };

    public async void ShowList()
    {
        await _windowManager.ShowWindowAsync(IoC.Get<ListViewModel>());
    }

    public void MenuItem_New()
    {
        _log.Information("Hi");
        _log.Information(SelectedComboBox ?? "nada");
    }

    public async void MenuItem_Settings()
    {
        await _windowManager.ShowWindowAsync(IoC.Get<SettingsViewModel>());
    }
}
