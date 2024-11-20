using Microsoft.AspNetCore.Mvc;

namespace bookflow.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
