using Gokarts.Controllers;
using Gokarts.Models;
using System.Diagnostics;

namespace Gokarts.ViewModels;

public class HistoryViewModel : ViewModelBase
{
    private readonly TournamentOccurence _occurence;
    private readonly Tournament _tournament;

    public string Id => _occurence.Id.ToString();
    public string TournamentId => _occurence.TournamentId.ToString();
    public string Name => _tournament.Name;
    public string Date => _occurence.Date;

    public HistoryViewModel(TournamentOccurence occurence)
    {
        _occurence = occurence;
        _tournament = DataBaseController.SelectTournament(occurence.TournamentId)[0];
    }
}
