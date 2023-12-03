using PlaceGuesser.Models;

namespace PlaceGuesser.Interfaces;

public interface IGamesRepository
{
    bool IsGameOver(int gameId);
    bool Contains(int gameId);
    GamePreferences GetPreferences(int gameId);
    int Add(GamePreferences preferences);
    void CreateNewRound(int gameId, Video video);
    Coordinates GetCoordsOfLastRound(int guessId);
}