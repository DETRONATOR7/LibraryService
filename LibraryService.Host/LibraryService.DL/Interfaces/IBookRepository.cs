using LibraryService.Models.Dto;

namespace LibraryService.DL.Interfaces
{
    public interface IBookRepository
    {
        void Add(Book book);
        void Delete(Guid? id);
        List<Book> GetAll();
        Book? GetById(Guid? id);
        void Update(Book book);
    }
}
