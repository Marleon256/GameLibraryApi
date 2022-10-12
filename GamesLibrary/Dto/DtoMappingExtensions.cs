using GamesLibrary.Infrastructure.InfrustructureModels;
using GamesLibrary.Infrastructure.Repository;

namespace GamesLibrary.Dto;

public static class DtoMappingExtensions
{
    public static Game ToGame(this GameDto gameDto) => 
        new(gameDto.Id, gameDto.GameName, gameDto.Company.ToCompany(),  gameDto.Genres.Select(ToGenre).ToList());

    public static Genre ToGenre(this GenreDto genreDto) => new(genreDto.id, genreDto.GenreName);

    public static Company ToCompany(this CompanyDto companyDto) => new(companyDto.Id, companyDto.CompanyName);

    public static RepositoryFilter ToRepositoryFilter(this FilterDto filterDto) => 
        new(filterDto.Key, filterDto.Value, filterDto.Operation.ToFilterOperation());

    public static FilterOperation ToFilterOperation(this FilterOperationDto filterOperationDto)
    {
        switch (filterOperationDto)
        {
            case FilterOperationDto.Equal:
                return FilterOperation.Equal;
            default:
                throw new ArgumentException("Unknown filter operation");
        }
    }

    public static GameDto ToGameDto(this Game game) => 
        new(game.Id, game.Name, game.CompanyInformation.ToCompanyDto(), game.GameGenres.Select(ToGenreDto).ToList());

    public static CompanyDto ToCompanyDto(this Company company) => new(company.Id, company.Name);

    public static GenreDto ToGenreDto(this Genre genre) => new(genre.Id, genre.Name);
}
