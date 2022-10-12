namespace GamesLibrary.Infrastructure.InfrustructureModels;

public class Genre
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public Genre(int? id, string name)
    {
        Id = id;
        Name = name;
    }

    public Genre() { }
}
