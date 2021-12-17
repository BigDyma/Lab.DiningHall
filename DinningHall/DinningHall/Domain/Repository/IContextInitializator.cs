using System.Threading.Tasks;

namespace DinningHall.Domain.Repository
{
    public interface IContextInitializator
    {
        Task SetClientsForAllTables();
    }
}