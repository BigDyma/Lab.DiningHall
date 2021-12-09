using DinningHall.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinningHall.Domain.Repository
{
    public interface IBaseRepository
    {
        Task<List<Order>> AddOrder(Order order);
        Task<List<Waiter>> GetAvailableWaiters();
        Task<List<Table>> GetTables();
        Task<Order> SetOrder(Waiter waiter, Table table);
        Task<Table> UpdateTable(Table table);
        Task ServeOrder(Order order);
        Task InitContext();
    }
}