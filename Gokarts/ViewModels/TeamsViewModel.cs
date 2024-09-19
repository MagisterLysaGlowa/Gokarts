using Gokarts.Models;

namespace Gokarts.ViewModels;

public class TeamsViewModel : ViewModelBase
{
    private readonly Team _team;

    public string Id => _team.Id.ToString();
    public string Name => _team.Name;
    public string Img => _team.Img;

    public TeamsViewModel(Team team)
    {
        _team = team;
    }
}
