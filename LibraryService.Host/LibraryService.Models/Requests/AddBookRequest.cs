namespace LibraryService.Models.Requests
{
    public class AddBookRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }
    }
}
