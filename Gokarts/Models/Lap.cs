namespace Gokarts.Models;

public class Lap
{
    public int Id { get; set; }
    public int Time { get; set; }
    public int RunId { get; set; }

    public Lap(int id, int time, int runId)
    {
        Id = id;
        Time = time;
        RunId = runId;
    }
}
