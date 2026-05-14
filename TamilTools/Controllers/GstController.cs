using Microsoft.AspNetCore.Mvc;

namespace TamilTools.Controllers
{
    public class GstController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}