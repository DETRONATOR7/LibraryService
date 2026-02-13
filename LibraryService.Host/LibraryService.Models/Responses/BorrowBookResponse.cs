using LibraryService.Models.Dto;

namespace LibraryService.Models.Responses
{
    public class BorrowBookResponse
    {
        public Book Book { get; set; } = new();
        public Member Member { get; set; } = new();
        public DateTime DueDate { get; set; }
    }
}
