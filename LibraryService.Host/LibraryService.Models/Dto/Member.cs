namespace LibraryService.Models.Dto
{
    public class Member
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Максимален брой активни заеми (примерна бизнес логика).
        /// </summary>
        public int MaxActiveLoans { get; set; } = 3;
    }
}
