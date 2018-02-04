using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class HashValidationViewModel : ApplicationUser
    {
        public string HostUrl { get; set; }
        public string Hash { get; set; }
    }
}
