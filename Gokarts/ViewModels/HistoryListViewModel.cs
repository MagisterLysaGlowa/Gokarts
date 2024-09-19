using Gokarts.Commands;
using Gokarts.Controllers;
using Gokarts.Models;
using Gokarts.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Gokarts.ViewModels;

public class HistoryListViewModel : ViewModelBase
{
    public ObservableCollection<HistoryViewModel> TournamentsOccurence { get; }
    public ICommand NavigateHomeCommand { get; }

    public HistoryListViewModel(NavigationStore navigationStore)
    {
        NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));

        ObservableCollection<TournamentOccurence> tournaments = DataBaseController.SelectTournamentOccurences();
        TournamentsOccurence = new();
        foreach (TournamentOccurence tournament in tournaments)
        {
            TournamentsOccurence.Add(new HistoryViewModel(tournament));
        }
    }
}
