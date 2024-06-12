using Data.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{

    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task InitialiseAsync()
        {
            await _context.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
          
            // Default data
            // Seed, if necessary
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product
                {
                    Color = new Color { Name = "Red" },
                    Size = new Size { Name = "Small" },
                    Price = 10.00m,
                    Description = "A small red product"
                });

                _context.Products.Add(new Product
                {
                    Color = new Color { Name = "Blue" },
                    Size = new Size { Name = "Medium" },
                    Price = 20.00m,
                    Description = "A medium blue product"
                });

                _context.Products.Add(new Product
                {
                    Color = new Color { Name = "Green" },
                    Size = new Size { Name = "Large" },
                    Price = 30.00m,
                    Description = "A large green product"
                });

                _context.Products.Add(new Product
                {
                    Color = new Color { Name = "Yellow" },
                    Size = new Size { Name = "Extra Large" },
                    Price = 40.00m,
                    Description = "An extra large yellow product"
                });

                await _context.SaveChangesAsync();
            }
        }


    }
}
