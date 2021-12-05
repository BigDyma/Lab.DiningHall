using DinningHall.Models;
using System.Threading.Tasks;

namespace DinningHall.Service
{
    public interface IRequestService
    {

        Task SendOrder(Waiter waiter, Order order, Table table);


    }
}