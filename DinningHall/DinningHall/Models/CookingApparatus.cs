using DinningHall.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.Models
{
    public class CookingApparatus : BaseEntity
    {
        public CookingApparatusType Type { get; set; }

        public CookingApparatus():base()
        {

        }
    }
}
