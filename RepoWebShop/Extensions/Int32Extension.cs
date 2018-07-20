using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class Int32Extension
    {
		public static IEnumerable<KeyValuePair<uint, uint>> Paginate(this uint elements, uint pageSize)
		{
			if (pageSize == 0)
				pageSize = 1;

			var result = new List<KeyValuePair<uint, uint>>().ToArray().AsEnumerable();

			var pages = Enumerable.Range(1, Convert.ToInt32(Math.Ceiling(elements / (decimal)pageSize)));

			foreach (var page in pages)
			{
				if (elements >= pageSize)
				{
					elements -= pageSize;
					result = result.Append(new KeyValuePair<uint, uint>(elements, (uint)pageSize));
				}
				else
					result = result.Append(new KeyValuePair<uint, uint>(0, elements));
			}

			return result;
		}
    }
}
