using System;
using Caliburn.Micro;

namespace WpfAppCore.Models;

public class Person : PropertyChangedBase
{
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
        Console.WriteLine($"Hello {Name} {Surname} of age {Age.ToString()}");
    }
}
