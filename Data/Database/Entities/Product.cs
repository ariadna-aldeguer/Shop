using Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database.Entities
{
    public class Product : BaseEntity
    {
        public Guid SizeId { get; set; }
        public virtual Size Size { get; set; }
        public Guid ColorId { get; set; }
        public virtual Color Color { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
