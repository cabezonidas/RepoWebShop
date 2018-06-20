using System;
using System.Collections.Generic;

namespace RepoWebShop.Models
{
    public class PayerDataRevenue
    {
        public int CbteTipo { get; }
        public int DocTipo { get; }
        public long DocNro { get; }
        public bool ValidPayer { get; }
        public decimal OrderTotal { get; }

        public PayerDataRevenue(Order order)
        {
            OrderTotal = order.OrderTotal;
            ValidPayer = true;
            CbteTipo = order.Cuit > 0 ? 1 : 6; //factura B es 6 y factura A es 1

            if (CbteTipo == 1)
            {
                DocTipo = 80; //CUIT
                DocNro = order.Cuit;
                return;
            }

            if (CbteTipo == 6 && order.OrderTotal < 1000)
            {
                DocTipo = 99; //Sin informar
                DocNro = 0;
                return;
            }

            KeyValuePair<int, long> id = new KeyValuePair<int, long>(0, 0);
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


            if (CbteTipo == 6 && order.OrderTotal > 1000 && ValidPayer)
            {
                DocTipo = id.Key;
                DocNro = id.Value;
                return;
            }

            ValidPayer = false;
        }
    }
}
