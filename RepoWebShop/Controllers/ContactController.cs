using Microsoft.AspNetCore.Mvc;

namespace RepoWebShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
