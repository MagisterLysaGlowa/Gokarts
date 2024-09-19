namespace Gokarts.Models;

public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Img { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public Tournament(int id, string name, string img, bool isActive, string description, string type)
    {
        Id = id;
        Name = name;
        Img = img;
        IsActive = isActive;
        Description = description;
        Type = type;
    }

    public Tournament() { }
}
