using Gokarts.Commands;
using Gokarts.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Gokarts.ViewModels;

public class PlayersAndTeamsListViewModel : ViewModelBase
{
    private readonly ObservableCollection<TeamsViewModel> _teams;
    private readonly ObservableCollection<PlayersViewModel> _players;
    private bool _isVisible;
    
    public bool IsVisible 
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            OnPropertyChanged(nameof(IsVisible));
        }        
    }
    public IEnumerable<TeamsViewModel> Teams => _teams;
    public IEnumerable<PlayersViewModel> Players => _players;
    public ICommand NavigateHomeCommand { get; }
    public ICommand VisibilityCommand { get; }

/*        private void ChangeVisibility()
    {
        if (IsVisible == Visibility.Hidden)
            _isVisible = Visibility.Visible;
        else
            _isVisible = Visibility.Hidden;
    }*/
    public PlayersAndTeamsListViewModel(NavigationStore navigationStore)
    {
        NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
        VisibilityCommand = new VisibilityCommand(() => _isVisible = !_isVisible);
        _teams = new ObservableCollection<TeamsViewModel>();
        _players = new ObservableCollection<PlayersViewModel>();
        _isVisible = true;

    }
}
