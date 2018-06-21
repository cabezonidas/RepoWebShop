using System;
using System.Collections.Generic;

namespace RepoWebShop.Models
{
    public class InvoiceDetail
    {
        public InvoiceDetail(string type, InvoiceData invoiceData, FECAEResponse.CodeMessage codeMessage)
        {
            InvoiceData = invoiceData;
            Type = type;
            Code = codeMessage.Code;
            Msg = codeMessage.Msg;
        }

        public InvoiceDetail()
        {
        }

        public int InvoiceDetailId { get; set; }
        public int InvoiceDataId { get; set; }
        public InvoiceData InvoiceData { get; set; }
        public string Type { get; set; }
        public int Code { get; set; }
        public string Msg { get; set; }

    }
}
