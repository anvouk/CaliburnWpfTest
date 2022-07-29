using Caliburn.Micro;

namespace WpfAppCore.ViewModels;

public class EditViewModel : Screen
{
    private string _age = "";
    private string _name = "";

    private string _surname = "";

    public string Name
    {
        get => _name;
        set
        {
            if (Set(ref _name, value)) NotifyOfPropertyChange(nameof(CanConfirm));
        }
    }

    public string Surname
    {
        get => _surname;
        set
        {
            if (Set(ref _surname, value)) NotifyOfPropertyChange(nameof(CanConfirm));
        }
    }

    public string Age
    {
        get => _age;
        set
        {
            if (Set(ref _age, value)) NotifyOfPropertyChange(nameof(CanConfirm));
        }
    }

    public bool CanConfirm =>
        !string.IsNullOrEmpty(Name) &&
        !string.IsNullOrEmpty(Surname) &&
        !string.IsNullOrEmpty(Age) &&
        int.TryParse(Age, out _);

    public async void Confirm()
    {
        await TryCloseAsync(true);
    }

    public async void Abort()
    {
        await TryCloseAsync(false);
    }
}
