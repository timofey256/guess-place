using Microsoft.AspNetCore.Mvc;
using PlaceGuesser.Models;
using PlaceGuesser.Repositories;

namespace PlaceGuesser.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly ILogger<GamesController> _logger;
    private GamesRepository _currentGames = new GamesRepository();

    public GamesController(ILogger<GamesController> logger)
    {
        _logger = logger;
    }

    [HttpPut("/create-game/")]
    public IActionResult CreateNewGame([FromBody] GamePreferences preferences)
    {
        int gameID = _currentGames.Add(preferences);
        return Ok(gameID);
    }
    
    [HttpGet("/get-video/")]
    public IActionResult GetVideoForRound([FromBody] int gameID)
    {
        if (_currentGames.IsGameOver(gameID))
        {
            return BadRequest("Game is already over");
        }

        _currentGames.IncreaseRound(gameID);
        GamePreferences preferences = _currentGames.GetPreferences(gameID);
        VideoRepository.GetVideo(preferences);
        return Ok(gameID);
    }
    
    [HttpPost("/guess-location/")]
    public IActionResult GuessLocation([FromBody] int gameID, [FromBody] Coordinates guess)
    {
        if (!_currentGames.Cointais(gameID)) return BadRequest("Bad ID : There is no such game.");
        Coordinates actual = _currentGames.getCoordsOfLastRound(gameID);
        return Ok(CoordinatesComparer.CompareCoordinates(actual, guess));
    }
}