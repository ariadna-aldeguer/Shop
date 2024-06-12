using Data.Database;
using Data.Database.Entities;
using Data.Database.Validations;
using Data.Interficies;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IValidation<Color> _validation;

        public ColorRepository(ApplicationDbContext context, IValidation<Color> validation) : base(context, validation)
        {
            _db = context;
            _validation = validation;
        }

        public async Task<Color> GetByNameAsync(string name)
        {
            return await _db.Colors.Where(s => s.Name == name).FirstOrDefaultAsync();
        }

    }
}
