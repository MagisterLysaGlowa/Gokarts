namespace Gokarts.Models;

public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DateOfBirth { get; set; }
    public int TeamId { get; set; }
    public string Img { get; set; }

    public Player(int id, string firstName, string lastName, string dateOfBirth, int teamId, string img)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        TeamId = teamId;
        Img = img;
    }
}
