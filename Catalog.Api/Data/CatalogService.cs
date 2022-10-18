using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogService : ICatalogService
    {
        private readonly IConfiguration configuration;

        public IMongoCollection<Product> Products { get; set; } = null!;

        public CatalogService(IConfiguration configuration)
        {
            this.configuration = configuration;

            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>
                (configuration.GetValue<string>("DatabaseSettings:CollectionName"));


        }
    }
}
