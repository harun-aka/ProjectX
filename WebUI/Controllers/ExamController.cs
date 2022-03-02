using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
