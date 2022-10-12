using GamesLibrary.Infrastructure.InfrustructureModels;
using System.Linq.Expressions;

namespace GamesLibrary.Infrastructure.Repository;

public interface IRepository<T>
{
    Task<List<T>> GetAll();

    Task<T> Get(int id);

    Task<T> Add(T entity);

    Task<T> Update(T entity);

    Task Delete(T entity);

    Task<List<T>> GetAllBySingleFilter(RepositoryFilter filter);
}