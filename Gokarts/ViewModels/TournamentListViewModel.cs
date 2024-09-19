using Gokarts.Commands;
using Gokarts.Controllers;
using Gokarts.Models;
using Gokarts.Stores;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Gokarts.ViewModels;

public class TournamentListViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private int _selectedIndex = -1;
    private bool _hasSelectedItem = false;
    private ObservableCollection<Tournament> _tournaments = new();
    private ObservableCollection<TournamentViewModel> _tournamentViewModels = new();
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            _selectedIndex = value;
            OnPropertyChanged(nameof(SelectedIndex));
            HasSelectedItem = _selectedIndex != -1;
        }
    }
    public bool HasSelectedItem
    {
        get => _hasSelectedItem;
        set
        {
            _hasSelectedItem = value;
            OnPropertyChanged(nameof(HasSelectedItem));
        }
    }
    public IEnumerable<TournamentViewModel> Tournaments => _tournamentViewModels;

    public ICommand NavigateHomeCommand { get; }
    public ICommand NavigateAddTournamentCommand { get; }
    public ICommand ActivateTournamentCommand { get; }
    public ICommand NavigateEditTournamentCommand { get; }
    public ICommand DeleteTournamentCommand { get; }

    public TournamentListViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
        NavigateAddTournamentCommand = new NavigateCommand<AddOrEditTournamentViewModel>(navigationStore, () => new AddOrEditTournamentViewModel(navigationStore, null, null));
        ActivateTournamentCommand = new DataBaseCommand(() =>
        {
            var tournament = _tournaments[SelectedIndex];
            tournament.IsActive = true;
            DataBaseController.UpdateTournament(tournament);
            _tournamentViewModels[SelectedIndex] = new TournamentViewModel(navigationStore, tournament);
        });
        NavigateEditTournamentCommand = new NavigateCommand<AddOrEditTournamentViewModel>(navigationStore, () =>
        {
            return new AddOrEditTournamentViewModel(navigationStore, _tournaments[SelectedIndex], null);
        });
        DeleteTournamentCommand = new DataBaseCommand(() =>
        {
            DataBaseController.DeleteTournament(_tournaments[SelectedIndex].Id);
            _tournaments.RemoveAt(SelectedIndex);
            _tournamentViewModels.RemoveAt(SelectedIndex);
        });
        UpdateListView();
    }

    public void UpdateListView()
    {
        _tournaments = DataBaseController.SelectTournaments();
        _tournamentViewModels = new ObservableCollection<TournamentViewModel>();
        foreach (Tournament tournament in _tournaments)
        {
            _tournamentViewModels.Add(new TournamentViewModel(_navigationStore, tournament));
        }
    }
}
