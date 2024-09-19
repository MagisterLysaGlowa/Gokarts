using Gokarts.Commands;
using Gokarts.Stores;
using System.Windows.Input;

namespace Gokarts.ViewModels;

public class ServiceViewModel : ViewModelBase
{
    public ICommand NavigateHomeCommand { get; }

    public ServiceViewModel(NavigationStore navigationStore) 
    {
        NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
    }
}
