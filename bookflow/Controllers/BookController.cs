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

        [HttpGet]
        public  ApiResponse GetBooks([FromQuery]string value)
        {
            try
            {
                return new ApiResponse(_blBook.GetBooks(value));
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex);
            }
        }

        [HttpDelete]
        public async Task<ApiResponse> DeleteBook([FromQuery] string id)
        {
            try
            {
                return new ApiResponse(await _blBook.DeleteBook(id));
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex);
            }
        }

        [HttpPut]
        public async Task<ApiResponse> UpdateBook([FromBody] Book book)
        {
            try
            {
                return new ApiResponse(await _blBook.UpdateBook(book));
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex);
            }
        }

        [HttpPost]
        public async Task<ApiResponse> CreateBook([FromBody] Book book)
        {
            try
            {
                return new ApiResponse(await _blBook.CreateBook(book));
            }
            catch (Exception ex)
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





    }
}
