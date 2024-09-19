namespace Gokarts.Models;

public class Team
{
    public int Id {  get; set; }
    public string Name { get; set; }
    public string Img { get; set; }

    public Team(int id, string name, string img)
    {
        Id = id;
        Name = name;
        Img = img;
    }
}
