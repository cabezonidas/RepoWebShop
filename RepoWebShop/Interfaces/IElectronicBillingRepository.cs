using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IElectronicBillingRepository
    {
        Task<InvoiceData> Facturar(Order order);
        Task<Cuit> ValidPersonaAsync(long id);
        Task<IEnumerable<InvoiceData>> GetAll(Func<InvoiceData, bool> condition = null);
        Task<InvoiceData> GetById(int id);
        IEnumerable<Cuit> CuitInfo(InvoiceData invoice);
		IEnumerable<Cae> AllCaes();
	}
}
