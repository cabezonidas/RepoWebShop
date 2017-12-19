using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Card()
        {
            return View();
        }
    }
}
