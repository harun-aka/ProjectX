using Microsoft.AspNetCore.Mvc;
using WebUI.Helpers;

namespace WebUI.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            if(AuthHelper.IsAuthenticated())
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }
    }
}
