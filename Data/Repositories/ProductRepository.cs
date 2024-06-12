using Data.Database;
using Data.Database.Entities;
using Data.Database.Validations;
using Data.Interficies;
using Data.Repositories;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IValidation<Product> _product;

        public ProductRepository(ApplicationDbContext context, IValidation<Product> validation) : base(context, validation)
        {
            _db = context;
            _product = validation;
        }

    }
}
