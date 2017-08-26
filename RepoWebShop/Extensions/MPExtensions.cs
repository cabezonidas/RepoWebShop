using mercadopago;
using System;
using System.Collections;

namespace RepoWebShop.Extensions
{
    public static class MPExtensions
    {
        public static string GetPaymentLink(this MP mp, decimal total, string bookingId, bool isPrd)
        {
            try
            {
                String preferenceData =
                        "{\"items\":" +
                            "[{" +
                                $"\"title\":\"La Reposteria ({bookingId})\"," +
                                "\"quantity\":1," +
                                $"\"id\":\"{bookingId}\"," +
                                "\"currency_id\":\"ARS\"," +
                                "\"unit_price\":" + total +
                            "}]," +
                        "\"back_urls\":" +
                            "{" +
                                $"\"success\":\"www.lareposteria.com.ar/Order/Status/{bookingId}\"," +
                                $"\"pending\":\"www.lareposteria.com.ar/Order/Status/{bookingId}\"," +
                                $"\"failure\":\"www.lareposteria.com.ar/Order/Status/{bookingId}\"" +
                            "}" +
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
