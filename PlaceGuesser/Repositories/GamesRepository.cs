using PlaceGuesser.Models;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;
using System.Text.Json;

namespace PlaceGuesser.Repositories;

public class GamesRepository
{
    private static readonly ConnectionMultiplexer Redis = ConnectionMultiplexer.Connect("localhost");
    private static int _currentIndex = 0;
    private IDatabase _db = Redis.GetDatabase();

    public GamesRepository() { }
        
    public int Add(GamePreferences preferences)
    {
        var newGame = new Game(preferences);
        string serializedGame = JsonSerializer.Serialize(newGame);
        _db.StringSet(_currentIndex.ToString(), serializedGame);
        _currentIndex += 1;
        return _currentIndex-1;
    }

    public Game? Get(int gameId)
    {
        string? serializedGame = _db.StringGet(gameId.ToString());
        return serializedGame is not null ? JsonSerializer.Deserialize<Game>(serializedGame) : null;
    }

    public bool IsGameOver(int gameId)
    {
        string? serializedGame = _db.StringGet(gameId.ToString());
        if (serializedGame == null) return true;
        
        var game = JsonSerializer.Deserialize<Game>(serializedGame);
        return game?.NumberOfRounds == game?.Rounds.Count;
    }

    public void CreateNewRound(int gameId, Video video)
    {
        throw new NotImplementedException();
    }

    public GamePreferences GetPreferences(int gameId)
    {
        string? serializedGame = _db.StringGet(gameId.ToString());
        if (serializedGame == null) throw new ArgumentException("Unexpected gameID. There's no such ID in DB");
        
        var game = JsonSerializer.Deserialize<Game>(serializedGame);
        return new GamePreferences() {Continent = game.Continent, NumberOfRounds = game.NumberOfRounds, VideoType = game.VideoType};
    }

    public Coordinates GetCoordsOfLastRound(int gameId)
    {
        string? serializedGame = _db.StringGet(gameId.ToString());
        if (serializedGame == null) throw new ArgumentException("Unexpected gameID. There's no such ID in DB");
        
        var game = JsonSerializer.Deserialize<Game>(serializedGame);
        return game.Rounds[^1].Coordinates;
    }

    public bool Contains(int gameId)
    {
        string? serializedGame = _db.StringGet(gameId.ToString());
        return serializedGame is not null;
    }
}