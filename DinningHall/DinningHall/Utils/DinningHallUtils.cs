using DinningHall.Models;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Service;

namespace DinningHall.Utils
{
    public abstract class DinningHallUtils
    {
        public static List<Waiter> GetWaiters(int waiterCount)
        {
            List<Waiter> waiters = new List<Waiter>();
            waiters.AddRange((new Waiter[waiterCount]).Select(o => new Waiter()));
            return waiters.ToList(); 
        }
        public static List<Food> getMenu()
        {
            return LoadJson();
        }
        public static List<Food> LoadJson()
        {

            var foods = new List<Food>();

            foods.AddRange(new List<Food>
            {
                new Food
                   {
                       Name = "Pizza",
                       PreparationTime = 20,
                       Complexity = 2,
                       CookingApparatusTypeName = "oven"
                   },
                new Food
                {
                    Name = "Salad",
                    PreparationTime = 10,
                    Complexity = 1,
                    CookingApparatusTypeName = null
                },
                new Food
                {
                    Name = "Zeama",
                    PreparationTime = 7,
                    Complexity = 1,
                    CookingApparatusTypeName = "stove"
                },
                new Food
                {
                    Name = "Scallop Sashami with Meyer Lemon Confit",
                    PreparationTime = 32,
                    Complexity = 3,
                    CookingApparatusTypeName = null
                },
                new Food
                {
                    Name = "Island Duck with Mulberry Mustard",
                    PreparationTime = 35,
                    Complexity = 3,
                    CookingApparatusTypeName = "oven"
                },
                new Food
                {
                    Name = "Waffles",
                    PreparationTime = 10,
                    Complexity = 1,
                    CookingApparatusTypeName = "stove"
                },
                new Food
                {
                    Name = "Aubergine",
                    PreparationTime = 20,
                    Complexity = 2,
                    CookingApparatusTypeName = null
                },
                new Food
                {
                    Name = "Lasagna",
                    PreparationTime = 30,
                    Complexity = 2,
                    CookingApparatusTypeName = "oven"
                },
                new Food
                {
                    Name = "Burger",
                    PreparationTime = 15,
                    Complexity = 1,
                    CookingApparatusTypeName = "oven"
                },
                new Food
                {
                    Name = "Gyros",
                    PreparationTime = 15,
                    Complexity = 1,
                    CookingApparatusTypeName = null
                }
            });

            return foods;

            //using (StreamReader r = new StreamReader("Menu.json"))
            //{
            //    string json = r.ReadToEnd();
            //    List<Food> items = JsonSerializer.Deserialize<List<Food>>(json);
            //    return items;
            //}
        }
        public static List<Table> GetTables( int amount)
        {
            List<Table> t = new List<Table>();
             t.AddRange((new Table[amount]).Select(o => new Table()));

            return t;
        }

        public static int GeneratePriority(Order order)
        {
            var priority = 1;
            if ((new int[] { 1, 4 }).ToList().Contains(order.Items.Count))
                priority += 2;

            if (order.MaxWaitTime <= 30)
                priority += 1;

            return priority;
        }

    }
}
