using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class InvoiceData
    {
        public int InvoiceDataId { get; set; }
        public DateTime Created { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public long Cuit { get; set; }
        public int PtoVta { get; set; }
        public int CbteTipo { get; set; }
        public string FchProceso { get; set; }
        public int CantReg { get; set; }
        public string Resultado { get; set; }
        public string Reproceso { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<Cae> Caes { get; set; }

        public void AddInvoiceDetailRange(IEnumerable<InvoiceDetail> invoiceDetails)
        {
            if (InvoiceDetails == null)
                InvoiceDetails = new Collection<InvoiceDetail>();

            foreach (var detail in invoiceDetails)
                InvoiceDetails.Add(detail);

        }

        internal void AddCaeRange(IEnumerable<Cae> caes)
        {
            if (Caes == null)
                Caes = new Collection<Cae>();

            foreach (var cae in caes)
                Caes.Add(cae);
        }
    }
}
