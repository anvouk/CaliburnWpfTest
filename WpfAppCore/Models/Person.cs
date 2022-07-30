using Caliburn.Micro;
using Serilog;

namespace WpfAppCore.Models;

public class Person : PropertyChangedBase
{
    private static readonly ILogger _log = Log.ForContext<Person>();

    private int _age;

    private string _name = "";

    private string _surname = "";

    public string Name
    {
        get => _name;
        set => Set(ref _name, value);
    }

    public string Surname
    {
        get => _surname;
        set => Set(ref _surname, value);
    }

    public int Age
    {
        get => _age;
        set => Set(ref _age, value);
    }

    public static Person CreateDefault()
    {
        return new Person
        {
            Name = "Change Me",
            Surname = "Change Me",
            Age = 0
        };
    }

    public void SayHi()
    {
        _log.Information($"Hello {Name} {Surname} of age {Age.ToString()}");
    }
}
