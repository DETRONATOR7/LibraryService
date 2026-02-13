using LibraryService.Models.Dto;

namespace LibraryService.BL.Interfaces
{
    public interface IBookCrudService
    {
        void Add(Book book);
        void Delete(Guid id);
        List<Book> GetAll();
        Book? GetById(Guid id);
        void Update(Guid id, Book book);
        void DecreaseAvailableCopies(Guid id);
    }
}
