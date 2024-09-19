namespace Gokarts.Models;

public class Penalty
{
    public int Id { get; set; }
    public int RunId { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }

    public Penalty(int id, int runId, string name, int value)
    {
        Id = id;
        RunId = runId;
        Name = name;
        Value = value;
    }
}
