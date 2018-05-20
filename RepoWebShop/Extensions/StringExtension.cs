using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class StringExtension
    {
        public static bool ContainsSubstring(this string text, string substring, bool ignorecase)
        {
            if (string.IsNullOrEmpty(substring))
                return true;
            if (string.IsNullOrEmpty(text))
                return false;
            if(!ignorecase)
            {
                text = text.ToLower();
                substring = substring.ToLower();
            }
            return text.IndexOf(substring) >= 0;
        }

        public static string Ending(this string text, int chars)
        {
            if (chars <= 0 || string.IsNullOrEmpty(text))
                return "";
            if (text.Length < chars)
                chars = text.Length;

            return text.Substring(text.Length - chars);
        }
    }
}
