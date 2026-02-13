namespace LibraryService.Models.Requests
{
    public class BorrowBookRequest
    {
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }

        /// <summary>
        /// За колко дни се дава книгата. По подразбиране 14.
        /// </summary>
        public int Days { get; set; } = 14;
    }
}
