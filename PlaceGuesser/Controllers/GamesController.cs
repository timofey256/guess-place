using Microsoft.AspNetCore.Mvc;
using PlaceGuesser.Models;
using PlaceGuesser.Repositories;

namespace PlaceGuesser.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly ILogger<GamesController> _logger;
    private readonly GamesRepository _currentGames = new GamesRepository();

    public GamesController(ILogger<GamesController> logger)
    {
        _logger = logger;
    }

    [HttpPut("/create-game/")]
    public IActionResult CreateNewGame([FromBody] GamePreferences preferences)
    {
        int gameId = _currentGames.Add(preferences);
        return Ok(gameId);
    }

    [HttpGet("/get-video/")]
    public IActionResult GetVideoForRound([FromBody] int gameId)
    {
        if (!_currentGames.Contains(gameId)) { return BadRequest("Bad ID : There is no such game."); }
        if (_currentGames.IsGameOver(gameId)) { return BadRequest("Game is already over"); }

        GamePreferences preferences = _currentGames.GetPreferences(gameId);
        Video video = VideoRepository.GetVideo(preferences);
        
        try
        {
            _currentGames.CreateNewRound(gameId, video);
        }
        catch (IndexOutOfRangeException e)
        {
            return BadRequest("You already played all rounds");
        }
        
        return Ok(video.Url);
    }
    
    [HttpPost("/guess-location/")]
    public IActionResult GuessLocation([FromBody] int gameId, [FromBody] Coordinates guess)
    {
        if (!_currentGames.Contains(gameId)) return BadRequest("Bad ID : There is no such game.");
        
        Coordinates actual = _currentGames.GetCoordsOfLastRound(gameId);
        return Ok(CoordinatesComparer.CompareCoordinates(actual, guess));
    }
}