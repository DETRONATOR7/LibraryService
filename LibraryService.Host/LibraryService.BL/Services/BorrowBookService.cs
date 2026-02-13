using LibraryService.BL.Interfaces;
using LibraryService.DL.Interfaces;
using LibraryService.Models.Responses;

namespace LibraryService.BL.Services
{
    internal class BorrowBookService : IBorrowBookService
    {
        private readonly IBookCrudService _bookCrudService;
        private readonly IMemberRepository _memberRepository;

        public BorrowBookService(IBookCrudService bookCrudService, IMemberRepository memberRepository)
        {
            _bookCrudService = bookCrudService;
            _memberRepository = memberRepository;
        }

        public BorrowBookResponse Borrow(Guid bookId, Guid memberId, int days = 14)
        {
            if (bookId == Guid.Empty) throw new ArgumentException("BookId is required.");
            if (memberId == Guid.Empty) throw new ArgumentException("MemberId is required.");
            if (days <= 0 || days > 60) throw new ArgumentException("Days must be between 1 and 60.");

            var book = _bookCrudService.GetById(bookId);
            var member = _memberRepository.GetById(memberId);

            if (book == null || member == null)
            {
                throw new ArgumentException($"Book ({bookId}) or Member ({memberId}) not found.");
            }

            // примерна бизнес логика: намаляваме наличните бройки
            _bookCrudService.DecreaseAvailableCopies(bookId);

            return new BorrowBookResponse
            {
                Book = book,
                Member = member,
                DueDate = DateTime.UtcNow.Date.AddDays(days)
            };
        }
    }
}
