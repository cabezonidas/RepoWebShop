using System;
using System.Collections.Generic;

namespace RepoWebShop.FeModels
{
    public class _Customer
	{
		public List<string> Emails { get; set; }
		public List<string> OrderIds { get; set; }
		public DateTime Created { get; set; }
		public string RegistrationId { get; set; }
		public string Name { get; set; }
		public string FacebookNameIdentifier { get; set; }
		public string GoogleNameIdentifier { get; set; }
		public bool EmailConfirmed { get; set; }
		public bool PhoneNumberConfirmed { get; set; }
		public string PhoneNumber { get; set; }
		public string PhoneNumberDeclared { get; set; }
		public int Orders { get; set; } = 0;
		public decimal Spent { get; set; }
	}
}
