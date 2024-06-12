using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interficies
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public IQueryable<T> AsQueryable();
        public Task<T> GetByIdAsync(Guid id);
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsByIdAsync(Guid id);
    }
}

