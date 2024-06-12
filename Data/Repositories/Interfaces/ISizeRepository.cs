using Data.Database.Entities;
using Data.Interficies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface ISizeRepository : IRepository<Size>
    {
        public Task<Size> GetByNameAsync(string name);
    }
}
