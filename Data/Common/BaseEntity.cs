using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastModified { get; set; }

    }

    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset LastModified { get; set; }
    }
}
