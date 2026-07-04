using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Error() => View();
    }
}
