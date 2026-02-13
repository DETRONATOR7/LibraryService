using LibraryService.BL.Interfaces;
using LibraryService.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowBookService _borrowBookService;

        public BorrowController(IBorrowBookService borrowBookService)
        {
            _borrowBookService = borrowBookService;
        }

        [HttpPost(nameof(Borrow))]
        public IActionResult Borrow([FromBody] BorrowBookRequest? request)
        {
            if (request == null) return BadRequest("Request is null.");

            try
            {
                var result = _borrowBookService.Borrow(request.BookId, request.MemberId, request.Days);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // за демо: връщаме 400 с причина
                return BadRequest(ex.Message);
            }
        }
    }
}
