namespace Gokarts.Models;

public class TournamentOccurence
{
    public int Id { get; set; }
    public int TournamentId { get; set; }
    public string Date { get; set; } = string.Empty;

    public TournamentOccurence(int id, int tournamentId, string date)
    {
        Id = id;
        TournamentId = tournamentId;
        Date = date;
    }

    public TournamentOccurence() { }

}
