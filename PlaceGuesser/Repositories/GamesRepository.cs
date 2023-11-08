using PlaceGuesser.Models;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;
using System.Text.Json;
using PlaceGuesser.Exceptions;

namespace PlaceGuesser.Repositories;

public class GamesRepository
{
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _db;
    private static int _currentIndex = 0;

    public GamesRepository()
    {
        try { _redis = ConnectionMultiplexer.Connect("localhost"); }
        catch (Exception e) { throw new AppRedisIsDownException(e.Message); }
        _db = _redis.GetDatabase();
    }

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
        if (serializedGame == null) throw new AppInvalidIdException(gameId);
        var game = JsonSerializer.Deserialize<Game>(serializedGame);
        return new GamePreferences() {Continent = game.Continent, NumberOfRounds = game.NumberOfRounds, VideoType = game.VideoType};
    }

    public Coordinates GetCoordsOfLastRound(int gameId)
    {
        string? serializedGame = _db.StringGet(gameId.ToString());
        if (serializedGame == null) throw new AppInvalidIdException(gameId);
        
        var game = JsonSerializer.Deserialize<Game>(serializedGame);
        return game.Rounds[^1].Coordinates;
    }

    public bool Contains(int gameId)
    {
        string? serializedGame = _db.StringGet(gameId.ToString());
        return serializedGame is not null;
    }
}