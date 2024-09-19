using Gokarts.Models;

namespace Gokarts.ViewModels;

public class PlayersViewModel : ViewModelBase
{
    private readonly Player _player;

    public string Id => _player.Id.ToString();
    public string FirstName => _player.FirstName;
    public string LastName => _player.LastName;
    public string DateOfBirth => _player.DateOfBirth;
    public string TeamId => _player.TeamId.ToString();
    public string Img => _player.Img;

    public PlayersViewModel(Player player)
    {
        _player = player;
    }
}
