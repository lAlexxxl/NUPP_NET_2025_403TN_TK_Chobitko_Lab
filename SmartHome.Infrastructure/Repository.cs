using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Infrastructure
{
    // Інтерфейс репозиторію (згідно з ТЗ)
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }

    // Реалізація репозиторію
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SmartHomeContext _context;

        public Repository(SmartHomeContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id) 
            => await _context.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() 
            => await _context.Set<T>().ToListAsync();

        public async Task AddAsync(T entity) 
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity) 
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity) 
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}