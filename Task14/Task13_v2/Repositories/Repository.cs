using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Task13.DataAccess;

namespace Task13_v2.Repositories
{
    public class Repository<T> where T : class
    {
        protected ApplicationDbContext db;
        private DbSet<T> table;

        public Repository(ApplicationDbContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            table.Update(entity);
        }

        public void Delete(T entity)
        {
            table.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true)
        {
            var entities = table.AsQueryable();
            if(expression != null)
            {
                entities = entities.Where(expression);
            }
            if(includes is not null)
            {
                foreach(var item in includes)
                {
                    entities = entities.Include(item);
                }
            }

            if(!tracked)
            {
                entities = entities.AsNoTracking();
            }
            return await entities.ToListAsync();
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true)
        {
            if(expression is not null)
            {
                return (await GetAsync(expression,includes,tracked)).FirstOrDefault();
            }
            return null;
        }

        public async Task CommitAsync()
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
