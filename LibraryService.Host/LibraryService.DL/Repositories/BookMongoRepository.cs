using LibraryService.DL.Interfaces;
using LibraryService.Models.Configurations;
using LibraryService.Models.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibraryService.DL.Repositories
{
    internal class BookMongoRepository : IBookRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<BookMongoRepository> _logger;
        private readonly IMongoCollection<Book> _booksCollection;

        public BookMongoRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            ILogger<BookMongoRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;

            var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);
            var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

            _booksCollection = database.GetCollection<Book>($"{nameof(Book)}s");
        }

        public void Add(Book book)
        {
            if (book == null) return;

            try
            {
                _booksCollection.InsertOne(book);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding book to DB: {Message} - {StackTrace}", e.Message, e.StackTrace);
            }
        }

        public void Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty) return;

            try
            {
                var result = _booksCollection.DeleteOne(b => b.Id == id);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning("No book found with Id: {BookId} to delete.", id);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error in {Method}: {Message} - {StackTrace}", nameof(Delete), e.Message, e.StackTrace);
            }
        }

        public List<Book> GetAll()
        {
            return _booksCollection.Find(_ => true).ToList();
        }

        public Book? GetById(Guid? id)
        {
            if (id == null || id == Guid.Empty) return default;

            try
            {
                return _booksCollection.Find(b => b.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError("Error in {Method}: {Message} - {StackTrace}", nameof(GetById), e.Message, e.StackTrace);
            }

            return default;
        }

        public void Update(Book book)
        {
            if (book == null || book.Id == Guid.Empty) return;

            try
            {
                _booksCollection.ReplaceOne(b => b.Id == book.Id, book);
            }
            catch (Exception e)
            {
                _logger.LogError("Error in {Method}: {Message} - {StackTrace}", nameof(Update), e.Message, e.StackTrace);
            }
        }
    }
}
