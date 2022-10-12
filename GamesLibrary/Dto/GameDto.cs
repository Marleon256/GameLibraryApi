namespace GamesLibrary.Dto;

public record GameDto(int? Id, string GameName, CompanyDto Company, List<GenreDto> Genres);
