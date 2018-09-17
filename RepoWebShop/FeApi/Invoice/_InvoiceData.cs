using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi.Invoice
{
	public class _InvoiceData
	{
		public int InvoiceDataId { get; set; }
		public int OrderId { get; set; }
		public IEnumerable<_Cae> Caes { get; set; }
		public IEnumerable<_InvoiceDetail> InvoiceDetails { get; set; }
		public DateTime Created { get; set; }
		public string Cuit { get; set; }
		public int PtoVta { get; set; }
		public int CbteTipo { get; set; }
		public string FchProceso { get; set; }
		public int CantReg { get; set; }
		public string Resultado { get; set; }
		public string Reproceso { get; set; }
		public string Factura { get; set; }
		public string FriendlyResultado { get; set; }
	}
}
