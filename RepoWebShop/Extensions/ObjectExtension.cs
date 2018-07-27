using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class ObjectExtension
    {
		public static string ToJson(this object obj)
		{
			MemoryStream ms = new MemoryStream();
			DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());
			ser.WriteObject(ms, obj);
			byte[] json = ms.ToArray();
			ms.Close();
			var data = Encoding.UTF8.GetString(json, 0, json.Length);
			return data;
		}
    }
}
