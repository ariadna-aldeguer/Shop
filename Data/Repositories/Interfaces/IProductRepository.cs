using Data.Database.Entities;
using Data.Interficies;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IProductRepository :  IRepository<Product>
    {
    }
}
