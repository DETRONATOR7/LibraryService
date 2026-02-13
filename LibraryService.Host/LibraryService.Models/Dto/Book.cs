namespace LibraryService.Models.Dto
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }
    }
}
