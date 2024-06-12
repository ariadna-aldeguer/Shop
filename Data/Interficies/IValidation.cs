using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interficies
{
    public interface IValidation<T> where T : class
    {
        public Task Validate(T item);
    }
}
