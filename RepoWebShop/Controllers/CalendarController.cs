using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using RepoWebShop.Models;

namespace RepoWebShop.Controllers
{
    public class CalendarController : Controller
    {
        public ViewResult Index()
        {

            return View();
        }
    }
}
