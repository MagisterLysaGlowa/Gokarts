using Gokarts.Commands;
using Gokarts.Models;
using Gokarts.Stores;
using System.Windows.Input;

namespace Gokarts.ViewModels;

public class TournamentViewModel : ViewModelBase
{
    private readonly Tournament tournament;

    public string Id => tournament.Id.ToString();
    public string Name => tournament.Name;
    public string Description => tournament.Description;
    public string Img => tournament.Img;
    public string Type => tournament.Type;
    public string IsActive => tournament.IsActive.ToString();
    public ICommand Edit { get; }

    public TournamentViewModel(NavigationStore navigationStore, Tournament tournament)
    {
        this.tournament = tournament;
        Edit = new NavigateCommand<AddOrEditTournamentViewModel>(navigationStore, () => new AddOrEditTournamentViewModel(navigationStore, this.tournament, () => { }));
    }
}
