using System.Threading.Tasks;
using DinningHall.Domain.Repository;
using DinningHall.Models;
using Microsoft.Extensions.Logging;

namespace DinningHall.Service
{
    public class DinningHallService : IDinningHallService
    {
        private readonly IBaseRepository _baseRepository;
        
        public DinningHallService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task ServeOrder(Order order)
        {
            if (order is object)
                await _baseRepository.ServeOrder(order);
        }
    }
}