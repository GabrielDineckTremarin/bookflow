using bookflow.DbSettings;
using Microsoft.AspNetCore.Mvc;
using bookflow.Business;
namespace bookflow.Controllers
{
    public class BookController : Controller
    {
        private readonly BlBook _blBook;
        public BookController(DbAccess dbAccess)
        {
            _blBook = new BlBook(dbAccess);
        }
        public IActionResult GetBook()
        {
            _blBook.teste();
            return Ok(new { Success = true, Message = "deu certo"});
        }

        public async Task<IActionResult> BorrowCopy([FromQuery]string id)
        {
            
            return Ok(new { Success = true, Data = await _blBook.BorrowCopy(id)});
        }

    }
}
