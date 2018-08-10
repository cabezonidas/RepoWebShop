using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeModels
{
    public class _PickUpOptions
	{
		public string Day { get; set; }
		public DateTime Date { get; set; }
		public IEnumerable<_DayOption> DayOptions { get; set; }
	}

	public class _DayOption
	{
		public string TicksId { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
	}
}
