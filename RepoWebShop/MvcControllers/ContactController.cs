using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Filters;

namespace RepoWebShop.MvcControllers
{
    [PageVisitAsync]
    public class ContactController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
