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

    private const string _badGameIdResponseMessage = "Bad ID : There is no game with id=p{0}.";
    private const string _gameIsOverMessage = "Game with id={0} is already over.";
    private const string _playedAllRoundsMessage = "You already played all rounds. Whole message: {0}";

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

    [HttpPost("/get-video/")]
    public IActionResult GetVideoForRound([FromBody] int gameId)
    {
        if (!_currentGames.Contains(gameId)) { return BadRequest(string.Format(_badGameIdResponseMessage, gameId)); }
        if (_currentGames.IsGameOver(gameId)) { return BadRequest(string.Format(_gameIsOverMessage, gameId)); }

        GamePreferences preferences = _currentGames.GetPreferences(gameId);
        Video video = VideoRepository.GetVideo(preferences);
        
        try
        {
            _currentGames.CreateNewRound(gameId, video);
        }
        catch (IndexOutOfRangeException e)
        {
            return BadRequest(string.Format(_playedAllRoundsMessage, e.Message));
        }
        
        return Ok(video.Url);
    }
    
    [HttpPost("/guess-location/")]
    public IActionResult GuessLocation([FromBody] Guess guess)
    {
        if (!_currentGames.Contains(guess.Id)) return BadRequest(string.Format(_badGameIdResponseMessage, guess.Id));
        Coordinates actual = _currentGames.GetCoordsOfLastRound(guess.Id);
        return Ok(CoordinatesComparer.CompareCoordinates(actual, Coordinates.ParseCoordinate(guess.Coordinates)));
    }
}