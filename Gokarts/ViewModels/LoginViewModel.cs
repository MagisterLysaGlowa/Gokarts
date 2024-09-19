using Gokarts.Commands;
using Gokarts.Controllers;
using Gokarts.Models;
using Gokarts.Stores;
using System.Windows.Input;

namespace Gokarts.ViewModels;

public class LoginViewModel : ViewModelBase
{
    public ICommand LogInCommand { get; }
    
    public LoginViewModel(NavigationStore navigationStore)
    {
        LogInCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
    }
}