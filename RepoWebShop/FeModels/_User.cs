using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeModels
{
	public class _User
	{
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string FacebookNameIdentifier { get; set; }
		public string GoogleNameIdentifier { get; set; }
		public string RegistrationId { get; set; }
	}
}
