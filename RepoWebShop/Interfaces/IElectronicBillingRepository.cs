using RepoWebShop.Models;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IElectronicBillingRepository
    {
        Task<FECAEResponse> FECAESolicitarAsync(Order order);
        Task<bool> ValidPersonaAsync(long id);
    }
}
