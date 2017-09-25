using Microsoft.AspNetCore.Mvc;

namespace RepoWebShop.Controllers
{
    public class AppExceptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
