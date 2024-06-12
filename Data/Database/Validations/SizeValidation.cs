using Data.Database.Entities;
using Data.Exceptions;
using Data.Interficies;
using Data.Repositories;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database.Validations
{
    public class SizeValidation : IValidation<Size>
    {
        public SizeValidation()
        {
        }

        public async Task Validate(Size size)
        {
            if (size == null)
            {
                throw new SizeException("There is no size.");
            }

            if (string.IsNullOrWhiteSpace(size.Name))
            {
                throw new SizeException("Size name is required.");
            }
        }

    }
}
