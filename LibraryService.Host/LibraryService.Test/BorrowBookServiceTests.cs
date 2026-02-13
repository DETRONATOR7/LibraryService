using LibraryService.BL.Interfaces;
using LibraryService.BL.Services;
using LibraryService.DL.Interfaces;
using LibraryService.Models.Dto;
using Moq;

namespace LibraryService.Test
{
    public class BorrowBookServiceTests
    {
        private Mock<IBookCrudService> _bookCrudServiceMock = null!;
        private Mock<IMemberRepository> _memberRepositoryMock = null!;

        [Fact]
        public void Borrow_Returns_Ok_And_Decreases_Copies()
        {
            // arrange
            _bookCrudServiceMock = new Mock<IBookCrudService>();
            _memberRepositoryMock = new Mock<IMemberRepository>();

            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();

            _bookCrudServiceMock
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(new Book
                {
                    Id = bookId,
                    Title = "Clean Code",
                    Isbn = "9780132350884",
                    PublicationYear = 2008,
                    AvailableCopies = 5
                });

            _bookCrudServiceMock
                .Setup(x => x.DecreaseAvailableCopies(It.IsAny<Guid>()));

            _memberRepositoryMock
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(new Member
                {
                    Id = memberId,
                    FullName = "John Doe",
                    Email = "john@doe.com",
                    MaxActiveLoans = 3
                });

            var service = new BorrowBookService(_bookCrudServiceMock.Object, _memberRepositoryMock.Object);

            // act
            var result = service.Borrow(bookId, memberId, days: 14);

            // assert
            Assert.NotNull(result);
            Assert.Equal(bookId, result.Book.Id);
            Assert.Equal(memberId, result.Member.Id);

            _bookCrudServiceMock.Verify(x => x.DecreaseAvailableCopies(bookId), Times.Once);
        }

        [Fact]
        public void Borrow_When_Member_Missing_Throws()
        {
            // arrange
            _bookCrudServiceMock = new Mock<IBookCrudService>();
            _memberRepositoryMock = new Mock<IMemberRepository>();

            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();

            _bookCrudServiceMock
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(new Book
                {
                    Id = bookId,
                    Title = "Clean Code",
                    Isbn = "9780132350884",
                    PublicationYear = 2008,
                    AvailableCopies = 5
                });

            _memberRepositoryMock
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns((Member?)null);

            var service = new BorrowBookService(_bookCrudServiceMock.Object, _memberRepositoryMock.Object);

            // act + assert
            Assert.Throws<ArgumentException>(() => service.Borrow(bookId, memberId, days: 14));

            _bookCrudServiceMock.Verify(x => x.DecreaseAvailableCopies(It.IsAny<Guid>()), Times.Never);
        }
    }
}
