namespace PlaceGuesser.Models;

public class Game
{
    public readonly int Id;
    public readonly GamePreferences Preferences;
    public readonly int NumberOfRounds;
    public List<Round> Rounds { get; private set; } = new List<Round>();

    public Game(int id, GamePreferences preferences)
    {
        Id = id;
        NumberOfRounds = preferences.NumberOfRounds;
        Preferences = preferences;
    }
}