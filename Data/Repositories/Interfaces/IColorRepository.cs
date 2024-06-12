using Data.Database.Entities;
using Data.Interficies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IColorRepository : IRepository<Color>
    {
        public Task<Color> GetByNameAsync(string name);
    }
}
