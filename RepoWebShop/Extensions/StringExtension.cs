using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
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

        public static string ToTitleCase(this string text)
        {
            text = text ?? string.Empty;
            text = text.TrimStart();
            text = text.TrimEnd();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            text = textInfo.ToTitleCase(text);
            return text;
        }

        public static bool IsValidEmail(this string email)
        {
            email = email ?? string.Empty;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
