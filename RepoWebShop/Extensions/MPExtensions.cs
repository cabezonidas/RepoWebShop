using mercadopago;
using System;
using System.Collections;

namespace RepoWebShop.Extensions
{
    public static class MPExtensions
    {
        public static string GetPaymentLink(this MP mp, decimal total, bool isPrd)
        {
            try
            {
                String preferenceData = "{\"items\":" +
                            "[{" +
                                "\"title\":\"La Reposteria\"," +
                                "\"quantity\":1," +
                                //"\"id\":" + orderId +
                                "\"currency_id\":\"ARS\"," +
                                "\"unit_price\":" + total +
                            "}]" +
                        "}";

                //mp.sandboxMode(true);

                Hashtable preference = mp.createPreference(preferenceData);
                string init_point = (isPrd ? "" : "sandbox_") + "init_point";
                
                return (preference["response"] as Hashtable)[init_point].ToString();
            }
            catch
            {
                return "#";
            }
        }
        
    }
}
