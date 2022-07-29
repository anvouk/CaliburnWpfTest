using System.Collections.ObjectModel;
using Caliburn.Micro;
using WpfAppCore.Models;

namespace WpfAppCore.ViewModels;

public class ListViewModel : Screen
{
    private readonly IWindowManager _windowManager;

    private ObservableCollection<Person> _myList = new();

    private Person? _selectedMyList;

    public ListViewModel(IWindowManager windowManager)
    {
        _windowManager = windowManager;
    }

    public ObservableCollection<Person> MyList
    {
        get => _myList;
        set => Set(ref _myList, value);
    }

    public Person? SelectedMyList
    {
        get => _selectedMyList;
        set
        {
            if (Set(ref _selectedMyList, value)) NotifyOfPropertyChange(nameof(CanRemoveElement));
        }
    }

    public bool CanRemoveElement => SelectedMyList != null;

    public bool CanRemoveAll => MyList.Count > 0;

    public void AddElement()
    {
        MyList.Add(Person.CreateDefault());
        NotifyOfPropertyChange(nameof(CanRemoveElement));
        NotifyOfPropertyChange(nameof(CanRemoveAll));
    }

    public void RemoveElement()
    {
        MyList.Remove(SelectedMyList);
        NotifyOfPropertyChange(nameof(CanRemoveElement));
        NotifyOfPropertyChange(nameof(CanRemoveAll));
    }

    public void RemoveAll()
    {
        MyList.Clear();
        NotifyOfPropertyChange(nameof(CanRemoveAll));
    }

    public async void Edit()
    {
        if (SelectedMyList == null) return;

        EditViewModel? editViewModel = IoC.Get<EditViewModel>();
        editViewModel.Name = SelectedMyList.Name;
        editViewModel.Surname = SelectedMyList.Surname;
        editViewModel.Age = SelectedMyList.Age.ToString();

        if (await _windowManager.ShowDialogAsync(editViewModel) == true)
        {
            SelectedMyList.Name = editViewModel.Name;
            SelectedMyList.Surname = editViewModel.Surname;
            SelectedMyList.Age = int.Parse(editViewModel.Age);
            SelectedMyList.SayHi();
        }
    }
}
