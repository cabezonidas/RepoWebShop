using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class StringExtension
    {
		public static string RemoveAccents(this string text)
		{
			if (string.IsNullOrWhiteSpace(text))
				return text;

			text = text.Normalize(NormalizationForm.FormD);
			var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
			var result = new string(chars).Normalize(NormalizationForm.FormC);

			return result;
		}

		public static string[] ToCharsPerLine(this string text, int charsPerLine)
		{
			var words = text.Split(" ").ToArray();

			var lines = new List<string>();
			var buffer = "";
			for (int i = 0; i < words.Length; i++)
			{
				if (buffer.Length + (words[i].Length + 1) <= charsPerLine)
					buffer += $"{(!string.IsNullOrEmpty(buffer) ? " " : "")}{words[i]}";
				else
				{
					lines.Add(buffer);
					buffer = words[i];
				}
				if (i == words.Length - 1)
					lines.Add(buffer);
			}
			var result = lines.ToArray();

			return result;
		}

        public static string CamelCaseString(this string camelCase)
        {
            if (string.IsNullOrEmpty(camelCase) || camelCase.Length < 2)
                return camelCase;

            List<char> chars = new List<char>();
            chars.Add(char.ToUpper(camelCase[0]));
            foreach (char c in camelCase.Skip(1))
            {
                if (char.IsUpper(c))
                {
                    chars.Add(' ');
                    chars.Add(char.ToLower(c));
                }
                else
                    chars.Add(c);
            }

            return new string(chars.ToArray());
        }

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

        public static T Parse<T>(this string json)
        {
			MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
			var obj = ser.ReadObject(ms);
			return (T)Convert.ChangeType(obj, typeof(T));
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
