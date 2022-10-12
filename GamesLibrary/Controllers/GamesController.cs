using GamesLibrary.Dto;
using GamesLibrary.Infrastructure.InfrustructureModels;
using GamesLibrary.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GamesLibrary.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IRepository<Game> _gameRepository;

    public GamesController(IRepository<Game> gameRepository)
    {
        _gameRepository = gameRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetAllGames([FromQuery] FilterDto filter)
    {
        List<Game> allGames;
        if (filter.Key == null || filter.Value == null)
        {
            allGames = await _gameRepository.GetAll();
        }
        else
        {
            allGames = await _gameRepository.GetAllBySingleFilter(filter.ToRepositoryFilter());
        }
        return allGames.Select(x => x.ToGameDto())
            .ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> GetGameById(int id)
    {
        var needGame = await _gameRepository.Get(id);
        return needGame.ToGameDto();
    }

    [HttpPost]
    public async Task<ActionResult<GameDto>> AddGame([FromBody] GameDto game)
    {
        return (await _gameRepository.Add(game.ToGame()))
            .ToGameDto();
    }

    [HttpPut]
    public async Task<ActionResult<GameDto>> UpdateGame([FromBody] GameDto gameDto)
    {
        if (!gameDto.Id.HasValue)
        {
            return BadRequest("No gamed Id to Update");
        }
        var game = await _gameRepository.Get(gameDto.Id.Value);
        if (game == null)
        {
            return NotFound();
        }
        return (await _gameRepository.Update(gameDto.ToGame()))
            .ToGameDto();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGame(int id)
    {
        var game = await _gameRepository.Get(id);
        if (game == null)
        {
            return NotFound();
        }    
        await _gameRepository.Delete(game);
        return Ok();
    }
}
