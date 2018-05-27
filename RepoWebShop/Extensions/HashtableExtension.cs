using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class HashtableExtension
    {
        public static bool HasValue(this Hashtable hashtable, string key) =>
            hashtable.ContainsKey(key) && hashtable[key] != null;

        public static dynamic GetValue(this Hashtable hashtable, string key, Type obj)
        {
            var resultType = obj.UnderlyingSystemType;
            object result = resultType == typeof(string) ? string.Empty : Activator.CreateInstance(resultType);
            

            if (hashtable.HasValue(key))
                try
                {
                    result = Convert.ChangeType(hashtable[key], resultType);
                }
                catch
                {
                }
            return result;
        }
    }
}
