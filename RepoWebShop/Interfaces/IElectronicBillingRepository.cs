using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IElectronicBillingRepository
    {
        Task<InvoiceData> Facturar(Order order);
        Task<bool> ValidPersonaAsync(long id);
        Task<IEnumerable<InvoiceData>> GetAll(Func<InvoiceData, bool> condition = null);
        Task<InvoiceData> GetById(int id);
    }
}
