using Gokarts.Commands;
using Gokarts.Stores;
using System.Windows.Input;

namespace Gokarts.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public ICommand NavigateServiceCommand { get; }
    public ICommand NavigatePATCommand { get; }
    public ICommand NavigateHistoryCommand { get; }
    public ICommand NavigateTournamentsCommand { get; }
    public HomeViewModel(NavigationStore navigationStore) 
    {
        NavigateServiceCommand = new NavigateCommand<ServiceViewModel>(navigationStore, () => new ServiceViewModel(navigationStore));
        NavigatePATCommand = new NavigateCommand<PlayersAndTeamsListViewModel>(navigationStore, () => new PlayersAndTeamsListViewModel(navigationStore));
        NavigateHistoryCommand = new NavigateCommand<HistoryListViewModel>(navigationStore, () => new HistoryListViewModel(navigationStore));
        NavigateTournamentsCommand = new NavigateCommand<TournamentListViewModel>(navigationStore, () => new TournamentListViewModel(navigationStore));
    }
}
