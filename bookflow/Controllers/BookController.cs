using Microsoft.AspNetCore.Mvc;

namespace bookflow.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetBook()
        {
            return Ok(new { Success = true, Message = "deu certo"});
        }
    }
}
