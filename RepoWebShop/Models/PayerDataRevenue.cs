using System;
using System.Collections.Generic;

namespace RepoWebShop.Models
{
    public class PayerDataRevenue
    {
        public int CbteTipo { get; set; }
        public int DocTipo { get; set; }
        public long DocNro { get; set; }
        public bool ValidPayer { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal[] SplitInvoices { get; set; }

        public PayerDataRevenue(Order order, int roof)
        {
            DocTipo = 99; //Sin informar
            DocNro = 0;
            OrderTotal = order.OrderTotal;

            SplitInvoices = new decimal[1];
            SplitInvoices[0] = order.OrderTotal;

            ValidPayer = true;
            CbteTipo = order.Cuit > 0 ? 1 : 6; //factura B es 6 y factura A es 1

            if (CbteTipo == 1)
            {
                DocTipo = 80; //CUIT
                DocNro = order.Cuit;
                return;
            }

            if (CbteTipo == 6 && order.OrderTotal < roof)
            {

                return;
            }

            KeyValuePair<int, long> id = new KeyValuePair<int, long>(99, 0);
            long docNro = 0;
            if (order.PayerIdType == "DNI")
                if (Int64.TryParse(order.PayerIdNumber, out docNro))
                    id = new KeyValuePair<int, long>(96, docNro);
                else
                    if (order.CardHolderType == "DNI")
                        if (Int64.TryParse(order.CardHolderNumber, out docNro))
                            id = new KeyValuePair<int, long>(96, docNro);
                        else
                            ValidPayer = false;


            if (CbteTipo == 6 && order.OrderTotal >= roof && ValidPayer)
            {
                DocTipo = id.Key;
                DocNro = id.Value;
                return;
            }

            if (CbteTipo == 6 && order.OrderTotal >= roof && !ValidPayer)
            {
                var chunks = Decimal.ToInt32(Math.Ceiling(order.OrderTotal / (roof - 0.01m)));
                var portion = Decimal.Round(order.OrderTotal / chunks, 2);
                var subtotal = 0m;
                SplitInvoices = new decimal[chunks];
                for(int i = 0; i < chunks; i++)
                {
                    if (i == chunks - 1)
                        SplitInvoices[i] = order.OrderTotal - subtotal;
                    else
                    {
                        SplitInvoices[i] = portion;
                        subtotal += portion;
                    }
                }

            }

            ValidPayer = false;
        }
    }
}
