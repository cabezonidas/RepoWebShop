using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class DecimalExtension
    {
        public static Decimal ApplyPercentage(this Decimal number, Decimal percentage)
        {
            return number * (1 + (percentage / 100m));
        }

        public static Decimal RoundTo(this Decimal number, int mod)
        {
            var result = Decimal.Round(number);
            var excess = result % mod;

            var roundUp = excess >= mod / 2m;
            result -= excess;

            return result += roundUp ? mod : 0;
        }
    }
}
