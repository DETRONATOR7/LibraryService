using LibraryService.BL.Interfaces;
using LibraryService.DL.Interfaces;
using LibraryService.Models.Dto;

namespace LibraryService.BL.Services
{
    internal class BookCrudService : IBookCrudService
    {
        private readonly IBookRepository _bookRepository;

        public BookCrudService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void Add(Book book)
        {
            if (book == null) return;

            if (book.Id == Guid.Empty)
            {
                book.Id = Guid.NewGuid();
            }

            _bookRepository.Add(book);
        }

        public void Delete(Guid id)
        {
            _bookRepository.Delete(id);
        }

        public List<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public Book? GetById(Guid id)
        {
            return _bookRepository.GetById(id);
        }

        public void Update(Guid id, Book book)
        {
            if (book == null) return;

            book.Id = id;
            _bookRepository.Update(book);
        }

        public void DecreaseAvailableCopies(Guid id)
        {
            var book = _bookRepository.GetById(id);

            if (book == null)
            {
                throw new ArgumentException($"Book with ID {id} not found.");
            }

            if (book.AvailableCopies <= 0)
            {
                throw new InvalidOperationException("No available copies for this book.");
            }

            book.AvailableCopies -= 1;
            _bookRepository.Update(book);
        }
    }
}
