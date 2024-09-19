namespace Gokarts.Models;

public class Run
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int TournamentId { get; set; }
    public int Time { get; set; }
    public string Description { get; set; }

    public Run(int id, int playerId, int tournamentId, int time, string description)
    {
        Id = id;
        PlayerId = playerId;
        TournamentId = tournamentId;
        Time = time;
        Description = description;
    }
}
