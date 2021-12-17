using DinningHall.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.Models
{
    public class Food : BaseEntity
    {
        public string Name { get; set; }

        public int PreparationTime { get; set; }

        public int Complexity { get; set; }

        public string CookingApparatusTypeName { get; set; }

        public CookingApparatusType? CookingApparatus => SetCookingApparatus();

        private CookingApparatusType? SetCookingApparatus()
        {
            if (CookingApparatusTypeName == "oven")
                return CookingApparatusType.Oven;
            if (CookingApparatusTypeName == "stove")
                return CookingApparatusType.Stove;

            return null;
        }

        public Food(): base()
        {

        }
    }
}
