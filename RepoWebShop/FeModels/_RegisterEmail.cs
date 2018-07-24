using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeModels
{
    public class _RegisterEmail
    {
		public string FirstName { get; set; } //Max length-50
		public string LastName { get; set; } //Max length-50
		public string Email { get; set; }
		public string Password { get; set; }
		public string ValidationCode { get; set; }
	}
}
