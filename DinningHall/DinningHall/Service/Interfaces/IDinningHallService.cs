using System.Threading.Tasks;
using DinningHall.Models;

namespace DinningHall.Service
{
    public interface IDinningHallService
    {
        Task ServeOrder(Order order);
    }
}