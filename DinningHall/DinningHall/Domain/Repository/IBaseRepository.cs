using DinningHall.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinningHall.Domain.Repository
{
    public interface IBaseRepository
    {
        List<Order> AddOrder(Order order);
        List<Waiter> GetAvailableWaiters();
        List<Table> GetTables();
        Order SetOrder(Waiter waiter, Table table);
        Table UpdateTable(Table table);
        Task ServeOrder(Order order);
    }
}