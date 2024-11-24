using bookflow.DbSettings;
using Microsoft.AspNetCore.Mvc;
using bookflow.Business;
using bookflow.Models;
using MongoDB.Driver;
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

        [HttpPost]
        public async Task<ApiResponse> BorrowCopy([FromQuery]string id, [FromQuery] DateTime returnDate)
        {
            
            try
            {
                return new ApiResponse(await _blBook.BorrowCopy(id, returnDate));
            }
            catch(Exception ex)
            {
                
                return new ApiResponse(ex);
            }
        }

        [HttpPost]
        public async Task<ApiResponse> MakeReservation([FromQuery] string id)
        {

            try
            {
                return new ApiResponse(await _blBook.MakeReservation(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex);
            }
        }

        [HttpPut]
        public async Task<ApiResponse> ReturnCopy([FromQuery] string id)
        {

            try
            {
                return new ApiResponse(await _blBook.ReturnCopy(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex);
            }
        }



    }
}
