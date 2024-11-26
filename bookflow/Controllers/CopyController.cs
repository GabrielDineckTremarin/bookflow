using bookflow.Business;
using bookflow.DbSettings;
using bookflow.Models;
using Microsoft.AspNetCore.Mvc;

namespace bookflow.Controllers
{
    public class CopyController : Controller
    {
        private readonly BlCopy _blCopy;
        public CopyController(DbAccess dbAccess)
        {
            _blCopy = new BlCopy(dbAccess);
        }

        [HttpPost]
        public async Task<ApiResponse> BorrowCopy([FromQuery] string id, [FromQuery] DateTime returnDate)
        {

            try
            {
                return new ApiResponse(await _blCopy.BorrowCopy(id, returnDate));
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
                return new ApiResponse(await _blCopy.ReturnCopy(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex);
            }
        }
    }
}
