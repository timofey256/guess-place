using PlaceGuesser.Data;
using PlaceGuesser.Interfaces;
using PlaceGuesser.Models;

namespace PlaceGuesser.Repositories;

public class VideosRepository : IVideosRepository
{
    private readonly VideoDBContext _context;

    public VideosRepository(VideoDBContext context)
    {
        _context = context;
    }

    public Video GetVideoBasedOnPreferences(GamePreferences preferences)
    {
        return new Video();
        //return _context.Videos.FirstOrDefault();
    }
}