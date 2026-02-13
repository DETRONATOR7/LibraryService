using FluentValidation;
using LibraryService.Models.Requests;

namespace LibraryService.Host.Validators
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(120);

            RuleFor(x => x.Isbn)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(17);

            RuleFor(x => x.PublicationYear)
                .InclusiveBetween(1450, DateTime.UtcNow.Year + 1);

            RuleFor(x => x.AvailableCopies)
                .InclusiveBetween(0, 999);
        }
    }
}
