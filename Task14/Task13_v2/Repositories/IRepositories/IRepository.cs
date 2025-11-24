using System.Linq.Expressions;

namespace Task13_v2.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
        Task<T> CreateAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task CommitAsync();

        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>[]? includes = null,
            bool tracked = true);

        Task<T?> GetOneAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>[]? includes = null,
            bool tracked = true);

        Task<IEnumerable<TResult>> JoinAsync<T2, TKey, TResult>(IRepository<T2> otherRepo, Expression<Func<T, TKey>> outerKey, Expression<Func<T2, TKey>> innerKey, Expression<Func<T, T2, TResult>> result) where T2 : class;

    }
}
