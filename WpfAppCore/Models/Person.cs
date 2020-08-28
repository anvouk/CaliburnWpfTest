using System;
using Caliburn.Micro;

namespace WpfAppCore.Models
{
    public class Person : PropertyChangedBase
    {
        public static Person CreateDefault()
        {
            return new Person
            {
                Name = "Change Me",
                Surname = "Change Me",
                Age = 0
            };
        }
        
        private string _name = "";
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _surname = "";
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }

        private int _age = 0;
        public int Age
        {
            get => _age;
            set => Set(ref _age, value);
        }

        public void SayHi()
        {
            Console.WriteLine($"Hello {Name} {Surname} of age {Age.ToString()}");
        }
    }
}
