namespace PlaceGuesser.Models;

public class Game
{
    public readonly Continent Continent;
    public readonly DayTime VideoType;
    public readonly int NumberOfRounds;
    public int Score { get; set; }
    public List<Round> Rounds { get; private set; } = new List<Round>();

    public Game(GamePreferences preferences)
    {
        NumberOfRounds = preferences.NumberOfRounds;
        Continent = preferences.Continent;
        VideoType = preferences.VideoType;
    }

    public void AddRound(Video video)
    {
        if (Rounds.Count >= NumberOfRounds)
            throw new IndexOutOfRangeException("Can't add new round.");
        
        Rounds.Add(new Round(video.VideoId, video.Coordinates));
    }
}