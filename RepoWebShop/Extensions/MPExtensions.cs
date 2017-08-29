using mercadopago;
using System;
using System.Collections;

namespace RepoWebShop.Extensions
{
    public static class MPExtensions
    {
        public static string GetPaymentLink(this MP mp, decimal total, string bookingId, bool isPrd, string host)
        {
            try
            {
                String preferenceData =
                        "{\"items\":" +
                            "[{" +
                                $"\"title\":\"La Reposteria - {bookingId}\"," +
                                "\"quantity\":1," +
                                $"\"id\":\"{bookingId}\"," +
                                "\"currency_id\":\"ARS\"," +
                                "\"unit_price\":" + total +
                            "}]," +
                        "\"back_urls\":" +
                            "{" +
                                $"\"success\":\"{host}/Order/Status/{bookingId}\"," +
                                $"\"pending\":\"{host}/Order/Status/{bookingId}\"," +
                                $"\"failure\":\"{host}/Order/Status/{bookingId}\"" +
                            "}" +
                        "}";

                mp.sandboxMode(!isPrd);

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
