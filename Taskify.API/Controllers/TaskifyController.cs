using Microsoft.AspNetCore.Mvc;

namespace Taskify.API.Controllers
{
    public class TaskifyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
