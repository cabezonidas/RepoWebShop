using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Filters;

namespace RepoWebShop.Controllers
{
    [PageVisitAsync]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
