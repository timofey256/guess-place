using Microsoft.AspNetCore.Mvc;
using PlaceGuesser.Models;
using PlaceGuesser.Interfaces;

namespace PlaceGuesser.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGamesRepository _gamesRepository; 
    private readonly IVideosRepository _videosRepository; 
    
    private const string BadGameIdResponseMessage = "Bad ID : There is no game with id=p{0}.";
    private const string GameIsOverMessage = "Game with id={0} is already over.";
    private const string PlayedAllRoundsMessage = "You already played all rounds. Whole message: {0}";

    public GamesController(IGamesRepository gamesRepository, IVideosRepository videosRepository)
    {
        _gamesRepository = gamesRepository;
        _videosRepository = videosRepository;
    }
    
    [HttpPut("/create-game/")]
    public IActionResult CreateNewGame([FromBody] GamePreferences preferences)
    {
        int gameId = _gamesRepository.Add(preferences);
        return Ok(gameId);
    }

    [HttpPost("/get-video/")]
    public IActionResult GetVideoForRound([FromBody] int gameId)
    {
        if (!_gamesRepository.Contains(gameId)) { return BadRequest(string.Format(BadGameIdResponseMessage, gameId)); }
        if (_gamesRepository.IsGameOver(gameId)) { return BadRequest(string.Format(GameIsOverMessage, gameId)); }

        GamePreferences preferences = _gamesRepository.GetPreferences(gameId);
        Video? video = _videosRepository.GetVideoBasedOnPreferences(preferences);
        
        try
        {
            _gamesRepository.CreateNewRound(gameId, video);
        }
        catch (IndexOutOfRangeException e)
        {
            return BadRequest(string.Format(PlayedAllRoundsMessage, e.Message));
        }
        
        return Ok(video?.SourceUrl);
    }
    
    [HttpPost("/guess-location/")]
    public IActionResult GuessLocation([FromBody] Guess guess)
    {
        if (!_gamesRepository.Contains(guess.Id)) return BadRequest(string.Format(BadGameIdResponseMessage, guess.Id));
        Coordinates actual = _gamesRepository.GetCoordsOfLastRound(guess.Id);
        return Ok(CoordinatesComparer.CompareCoordinates(actual, Coordinates.ParseCoordinate(guess.Coordinates)));
    }
}