using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    public class ProductException : BaseCustomException
    {
        public ProductException(string message)
            : base(message)
        {
        }
    }
}
