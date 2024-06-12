using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    public class SizeException : BaseCustomException
    {
        public SizeException(string message)
            : base(message)
        {
        }
    }
}
