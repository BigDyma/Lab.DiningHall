using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
