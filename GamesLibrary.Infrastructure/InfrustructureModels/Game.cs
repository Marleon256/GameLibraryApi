namespace GamesLibrary.Infrastructure.InfrustructureModels;

public class Game
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public Company CompanyInformation { get; set; }

    public List<Genre> GameGenres { get; set; }

    public Game(int? id, string name, Company companyInformation, List<Genre> gameGenres)
    {
        Id = id;
        Name = name;
        CompanyInformation = companyInformation;
        GameGenres = gameGenres;
    }

    public Game() { }
}
