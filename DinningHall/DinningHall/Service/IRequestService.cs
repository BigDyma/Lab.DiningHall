using DinningHall.Models;
using System.Threading.Tasks;

namespace DinningHall.Service
{
    public interface IRequestService
    {

        void SendOrder(Waiter waiter, Order order, Table table);


    }
}