using Data.Database;
using Data.Database.Entities;
using Data.Database.Validations;
using Data.Interficies;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class SizeRepository : GenericRepository<Size>, ISizeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IValidation<Size> _validation;

        public SizeRepository(ApplicationDbContext context, IValidation<Size> validation) : base(context, validation)
        {
            _db = context;
            _validation = validation;
        }

        public async Task<Size> GetByNameAsync(string name)
        {
            return await _db.Sizes.Where(s => s.Name == name).FirstOrDefaultAsync();
        }

    }
}
