using GamesLibrary.DataAccess.Enities;
using GamesLibrary.Infrastructure.InfrustructureModels;

namespace GamesLibrary.Infrastructure.Repository;

public static class MappingExtensions
{
    public static Game ToGame(this DbGame dbGame) =>
        new(dbGame.Id, dbGame.Name, dbGame.CompanyName?.ToCompany()!,
            dbGame.GamesMappingGenres?.Select(x => x.Genres?.ToGenre()).ToList()!);

    public static Genre ToGenre(this DbGenre dbGenre) => new(dbGenre.Id, dbGenre.Name);

    public static Company ToCompany(this DbCompany dbCompany) => new(dbCompany.Id, dbCompany.Name);

    public static DbCompany ToDbCompany(this Company company)
    {
        return new()
        {
            Id = company.Id.HasValue ? company.Id.Value : 0,
            Name = company.Name,
        };
    }

    public static DbGenre ToDbGenre(this Genre dbGenre)
    {
        return new()
        {
            Id = dbGenre.Id.HasValue ? dbGenre.Id.Value : 0,
            Name = dbGenre.Name
        };
    }

    public static DbGame ToDbGame(this Game game)
    {
        var dbGameToReturn = new DbGame
        {
            Id = game.Id.HasValue ? game.Id.Value : 0,
            Name = game.Name,
            CompanyName = game.CompanyInformation.ToDbCompany(),
        };
        dbGameToReturn.GamesMappingGenres = game.GameGenres
            .Select(ToDbGenre)
            .Select(x =>
                new DbGamesMappingGenre()
                {
                    Games = dbGameToReturn,
                    GamesId = dbGameToReturn.Id,
                    Genres = x,
                    GenresId = x.Id
                })
            .ToList();
        return dbGameToReturn;
    }
}
