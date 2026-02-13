using FluentValidation;
using LibraryService.BL.Interfaces;
using LibraryService.Models.Dto;
using LibraryService.Models.Requests;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookCrudService _bookCrudService;
        private readonly IMapper _mapper;
        private readonly IValidator<AddBookRequest> _addValidator;

        public BooksController(
            IBookCrudService bookCrudService,
            IMapper mapper,
            IValidator<AddBookRequest> addValidator)
        {
            _bookCrudService = bookCrudService;
            _mapper = mapper;
            _addValidator = addValidator;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var books = _bookCrudService.GetAll();
            return Ok(books);
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("ID must be a valid Guid.");

            var book = _bookCrudService.GetById(id);
            if (book == null) return NotFound($"Book with ID {id} not found.");

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddBookRequest? request)
        {
            if (request == null) return BadRequest("Book data is null.");

            var result = _addValidator.Validate(request);
            if (!result.IsValid) return BadRequest(result.Errors);

            var book = _mapper.Map<Book>(request);
            _bookCrudService.Add(book);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Guid id, [FromBody] UpdateBookRequest? request)
        {
            if (id == Guid.Empty) return BadRequest("ID must be a valid Guid.");
            if (request == null) return BadRequest("Book data is null.");

            var existing = _bookCrudService.GetById(id);
            if (existing == null) return NotFound($"Book with ID {id} not found.");

            var updated = _mapper.Map<Book>(request);
            _bookCrudService.Update(id, updated);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("ID must be a valid Guid.");

            var existing = _bookCrudService.GetById(id);
            if (existing == null) return NotFound($"Book with ID {id} not found.");

            _bookCrudService.Delete(id);
            return Ok();
        }
    }
}
