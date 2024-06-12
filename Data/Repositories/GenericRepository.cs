using Data.Database;
using Data.Database.Validations;
using Data.Interficies;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidation<T> _validation;
        public GenericRepository(ApplicationDbContext context, IValidation<T> validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IQueryable<T> AsQueryable()
        {
            return  _context.Set<T>().AsQueryable();
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id) != null;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _validation.Validate(entity);
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await _validation.Validate(entity);
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id) 
        { 
            var entity = await GetByIdAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
