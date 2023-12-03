using System.Text.Json.Serialization;

namespace PlaceGuesser.Models;

public record class Game
{
    public Continent Continent { get; set;  }
    
    public DayTime VideoType { get; set; }
    
    public int NumberOfRounds { get; set; }
    public int Score { get; set; }
    public List<Round> Rounds { get; } = new List<Round>();
    
    [JsonConstructor]
    public Game() { }
    
    public Game(GamePreferences preferences)
    {
        Continent = preferences.Continent;
        VideoType = preferences.VideoType;
        NumberOfRounds = preferences.NumberOfRounds;
        Score = 0;
    }

    public void AddRound(Video video)
    {
        if (Rounds.Count >= NumberOfRounds)
            throw new IndexOutOfRangeException("Can't add new round.");
        
        Rounds.Add(new Round(video.VideoId, video.CoordinateN, video.CoordinateE));
    }
}