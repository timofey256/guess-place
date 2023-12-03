using PlaceGuesser.Models;

namespace PlaceGuesser.Interfaces;

public interface IVideosRepository
{
    Video GetVideoBasedOnPreferences(GamePreferences preferences);
}