using LibraryService.DL.Interfaces;
using LibraryService.Models.Configurations;
using LibraryService.Models.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibraryService.DL.Repositories
{
    internal class MemberMongoRepository : IMemberRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<MemberMongoRepository> _logger;
        private readonly IMongoCollection<Member> _membersCollection;

        public MemberMongoRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            ILogger<MemberMongoRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;

            var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);
            var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

            _membersCollection = database.GetCollection<Member>($"{nameof(Member)}s");
        }

        public void Add(Member member)
        {
            if (member == null) return;

            try
            {
                _membersCollection.InsertOne(member);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding member to DB: {Message} - {StackTrace}", e.Message, e.StackTrace);
            }
        }

        public List<Member> GetAll()
        {
            return _membersCollection.Find(_ => true).ToList();
        }

        public Member? GetById(Guid? id)
        {
            if (id == null || id == Guid.Empty) return default;

            try
            {
                return _membersCollection.Find(m => m.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError("Error in {Method}: {Message} - {StackTrace}", nameof(GetById), e.Message, e.StackTrace);
            }

            return default;
        }
    }
}
