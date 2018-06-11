using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IElectronicBillingRepository
    {
        Task<string> GetLoginTicket();
    }
}
