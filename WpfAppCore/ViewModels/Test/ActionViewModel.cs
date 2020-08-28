using System;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace WpfAppCore.ViewModels
{
    public class ActionViewModel : Screen
    {
        private string _output;

        public void Clear() => Output = String.Empty;

        public void SimpleSayHello() => Output = "Hello from Caliburn.Micro";

        public void SayHello(string name) => Output = $"Hello {name}";

        public bool CanSayHello(string name) => !String.IsNullOrEmpty(name);

        public Task SayGoodbyeAsync(string name)
        {
            Output = $"Goodbye {name}";

            return Task.FromResult(true);
        }
        
        public bool CanSayGoodbye(string name) => !String.IsNullOrEmpty(name);

        public string Output
        {
            get => _output;
            set => Set(ref _output, value);
        }
    }
}
