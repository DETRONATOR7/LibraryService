using LibraryService.Models.Responses;

namespace LibraryService.BL.Interfaces
{
    public interface IBorrowBookService
    {
        BorrowBookResponse Borrow(Guid bookId, Guid memberId, int days = 14);
    }
}
