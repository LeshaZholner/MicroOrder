using MicroOrder.OrderService.API.Infrastructure.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MicroOrder.OrderService.API.Infrastructure;

public class OrderDbContext
{
    private readonly IMongoDatabase _database;

    public OrderDbContext(IMongoClient mongoClient, IOptions<MongoSettings> options)
    {
        _database = mongoClient.GetDatabase(options.Value.DatabaseName);
    }

    public IMongoCollection<Order> Orders => _database.GetCollection<Order>(nameof(Order));
}
