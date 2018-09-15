using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeModels
{
    public class _OrderCatering
    {
		public int Amount { get; set; }
		public _Catering Catering { get; set; }
	}
}
