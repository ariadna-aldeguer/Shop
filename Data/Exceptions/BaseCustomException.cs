using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    public abstract class BaseCustomException : Exception
    {
        protected BaseCustomException(string message) : base(message)
        {
        }
    }
}
