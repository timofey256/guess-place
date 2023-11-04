namespace PlaceGuesser.Models;

public class GamePreferences
{
    public Continent Continent { get; set; }
    public DayTime VideoType { get; set; }
    public int NumberOfRounds { get; set; }
}