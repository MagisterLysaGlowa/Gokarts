namespace Gokarts.Models;

public class DataModel
{
    public int FullLap { get; set; } //All lap
    public int LapCounter { get; set; } //Lap countdown
    public bool IsStart { get; set; }
    public bool Foto_1 { get; set; }
    public bool Foto_2 { get; set; }
    public bool Foto_3 { get; set; }
    public string LapTime { get; set; } = string.Empty;
    public string Time_1 { get; set; } = string.Empty;
    public string Time_2 { get; set; } = string.Empty;
    public string Time_3 { get; set; } = string.Empty;
    public bool ConfiguarationType { get; set; } //true = normal (school)
}
