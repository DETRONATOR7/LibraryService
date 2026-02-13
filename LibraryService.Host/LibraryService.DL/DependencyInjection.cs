using LibraryService.DL.Interfaces;
using LibraryService.DL.Repositories;
using LibraryService.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LibraryService.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configs)
        {
            // Guid <-> Mongo serialize
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            services
                .AddConfigurations(configs)
                .AddSingleton<IBookRepository, BookMongoRepository>()
                .AddSingleton<IMemberRepository, MemberMongoRepository>();

            return services;
        }

        private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configs)
        {
            services.Configure<MongoDbConfiguration>(configs.GetSection(nameof(MongoDbConfiguration)));
            return services;
        }
    }
}
