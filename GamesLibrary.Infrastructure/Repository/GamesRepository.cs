using GamesLibrary.DataAccess.Enities;
using GamesLibrary.Infrastructure.InfrustructureModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace GamesLibrary.Infrastructure.Repository;

public class GamesRepository : IRepository<Game>
{
    private readonly GamesContext _context;

    public GamesRepository(GamesContext context)
    {
        _context = context;
    }

    public async Task<Game> Add(Game game)
    {
        if (game == null) throw new ArgumentNullException("Trying to add null game");
        var dbGame = new DbGame()
        {
            Name = game.Name
        };
        dbGame.CompanyName = await GetDbCompanyFromGame(game);
        dbGame.GamesMappingGenres = await GetDbGameGenresFromGame(game);
        await _context.Games.AddAsync(dbGame);
        await _context.SaveChangesAsync();
        return dbGame.ToGame();
    }

    public async Task Delete(Game game)
    {
        if (game == null) throw new ArgumentNullException("Trying to delete null entity");
        if (!game.Id.HasValue) throw new ArgumentException("Cant delete without id");
        var dbGame = await _context.Games.Where(x => x.Id == game.Id)
            .SingleOrDefaultAsync();
        if (dbGame == null) 
            return;
        _context.Games.Remove(dbGame);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Game>> GetAllBySingleFilter(RepositoryFilter filter)
    {
        return (await _context.Games.Include(x => x.CompanyName)
            .Include(x => x.GamesMappingGenres)
            .ThenInclude(x => x.Genres)
            .Where(DecodeFilter(filter))
            .ToListAsync())
            .Select(x => x.ToGame())
            .ToList();
            
    }

    public async Task<Game> Get(int id)
    {
        var dbGame = await _context.Games.Include(x => x.CompanyName)
            .Include(x => x.GamesMappingGenres)
            .ThenInclude(x => x.Genres)
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
        if (dbGame == null)
        {
            return null!;
        }
        return dbGame.ToGame();
    }

    public async Task<List<Game>> GetAll()
    {
        return (await _context.Games.Include(x => x.CompanyName)
            .Include(x => x.GamesMappingGenres)
            .ThenInclude(x => x.Genres)
            .ToListAsync())
            .Select(x => x.ToGame())
            .ToList();
    }

    public async Task<Game> Update(Game game)
    {
        if (game == null) throw new ArgumentNullException("Trying to update null entity");
        if (!game.Id.HasValue) throw new ArgumentException("Cant update without id");
        var dbGame = await _context.Games.Where(x => x.Id == game.Id.Value)
            .SingleOrDefaultAsync();
        if (dbGame == null) throw new ArgumentException("Game to update not found");
        dbGame.Name = game.Name;
        dbGame.CompanyName = await GetDbCompanyFromGame(game);
        dbGame.GamesMappingGenres = await GetDbGameGenresFromGame(game);
        _context.Games.Update(dbGame);
        await _context.SaveChangesAsync();
        return dbGame.ToGame();
    }

    private async Task<DbCompany> GetDbCompanyFromGame(Game game)
    {
        if (game.CompanyInformation == null || game.CompanyInformation.Name == null)
            return null!;
        var existedDbCompany = await _context.Companies.Where(x => x.Name == game.CompanyInformation.Name)
            .SingleOrDefaultAsync();
        if (existedDbCompany == null)
        {
            return game.CompanyInformation.ToDbCompany();
        }
        else
        {
            return existedDbCompany;
        }
    }

    private async Task<List<DbGamesMappingGenre>> GetDbGameGenresFromGame(Game game)
    {
        if (game.GameGenres == null)
            return Enumerable.Empty<DbGamesMappingGenre>()
                .ToList();
        var dbGenres = new List<DbGenre>();
        for (int i = 0; i < game.GameGenres.Count; i++)
        {
            if (game.GameGenres[i].Name == null)
                continue;
            var existedDbGenre = await _context.Genres.Where(x => x.Name == game.GameGenres[i].Name)
                .SingleOrDefaultAsync();
            if (existedDbGenre == null)
            {
                dbGenres.Add(game.GameGenres[i].ToDbGenre());
            }
            else
            {
                dbGenres.Add(existedDbGenre);
            }
        }
        return dbGenres.Select(x => new DbGamesMappingGenre()
        {
            Genres = x
        }).ToList();
    }

    private Expression<Func<DbGame, bool>> DecodeFilter(RepositoryFilter filter)
    {
        switch (filter.Operation)
        {
            case FilterOperation.Equal:
                if (filter.Key == "Game.Genres.Name")
                    return x => x.GamesMappingGenres.Any(y => y.Genres.Name == filter.Value);
                else
                    throw new ArgumentException("Unknown key type for equal operation");
            default:
                throw new ArgumentException("Unknown filter type");
        }
    }
}
