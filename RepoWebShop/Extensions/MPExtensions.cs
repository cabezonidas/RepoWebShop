using mercadopago;
using System;
using System.Collections;

namespace RepoWebShop.Extensions
{
    public static class MPExtensions
    {
        public static string getPaymentLink(this MP mp, decimal total, bool isPrd)
        {
            try
            {
                String preferenceData = "{\"items\":" +
                            "[{" +
                                "\"title\":\"La Reposteria\"," +
                                "\"quantity\":1," +
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
