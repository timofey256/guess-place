using PlaceGuesser.Models;

namespace PlaceGuesser.Repositories;

public class GamesRepository
{
    private int _currentIndex = 0;
    private Dictionary<int, Game> _games = new Dictionary<int, Game>();

    public GamesRepository() { }
    
    public int Add(GamePreferences preferences)
    {
        var newGame = new Game(preferences);
        _games.Add(_currentIndex, newGame);
        _currentIndex++;
        return _currentIndex-1;
    }

    public bool IsGameOver(int gameId)
    {
        var game = _games[gameId];
        return game.Rounds.Count == game.NumberOfRounds;
    }

    public void CreateNewRound(int gameId, Video video)
    {
        var game = _games[gameId];
        game.AddRound(video);
    }

    public GamePreferences GetPreferences(int gameId)
    {
        var game = _games[gameId];
        return new GamePreferences() {Continent = game.Continent, VideoType = game.VideoType, NumberOfRounds = game.NumberOfRounds};
    }

    public Coordinates GetCoordsOfLastRound(int gameId)
    {
        var game = _games[gameId];
        return game.Rounds[^1].Coordinates;
    }

    public bool Contains(int gameId)
    {
        return _games.ContainsKey(gameId);
    }
}