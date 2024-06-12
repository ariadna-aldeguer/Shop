using Data.Database.Entities;
using Data.Exceptions;
using Data.Interficies;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database.Validations
{
    public class ColorValidation : IValidation<Color>
    {
        public ColorValidation() { }
        public async Task Validate(Color color)
        {
            if (color == null)
            {
                throw new ColorException("There is no color.");
            }

            if (string.IsNullOrWhiteSpace(color.Name))
            {
                throw new ColorException("Color name is required.");
            }
        }

    }
}
