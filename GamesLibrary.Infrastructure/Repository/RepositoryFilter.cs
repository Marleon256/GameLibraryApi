namespace GamesLibrary.Infrastructure.Repository;

public class RepositoryFilter
{
    public string Key { get; set; }

    public string Value { get; set; }

    public FilterOperation Operation { get; set; }

    public RepositoryFilter(string key, string value, FilterOperation operation)
    {
        Key = key;
        Value = value;
        Operation = operation;
    }

    public RepositoryFilter() { }
}
