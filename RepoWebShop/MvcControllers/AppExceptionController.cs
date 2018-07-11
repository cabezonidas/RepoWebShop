using Microsoft.AspNetCore.Mvc;

namespace RepoWebShop.MvcControllers
{
    public class AppExceptionController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
            //return View();
            //Log!
        }
    }
}
