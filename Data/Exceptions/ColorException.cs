using System;
using Data.Database.Entities;
namespace Data.Exceptions
{
    public class ColorException : BaseCustomException
    {
        public ColorException(string message)
            : base(message)
        {
        }
       
    }
}
