using MicroOrder.OrderService.API.Infrastructure.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace MicroOrder.OrderService.API.Infrastructure;

public static class MongoClassConventions
{
    public static void RegisterClassConventions()
    {
        BsonClassMap.RegisterClassMap<Order>(x =>
        {
            x.AutoMap();
            x.MapIdMember(x => x.Id)
                .SetIdGenerator(GuidGenerator.Instance)
                .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            x.MapMember(x => x.TotalAmount)
                .SetSerializer(new DecimalSerializer(BsonType.Decimal128));
            x.MapMember(x => x.CreatedAt)
                .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc));
        });

        BsonClassMap.RegisterClassMap<OrderItem>(x =>
        {
            x.AutoMap();
            x.MapIdMember(x => x.Id)
                .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            x.MapMember(x => x.Price)
                .SetSerializer(new DecimalSerializer(BsonType.Decimal128));
        });
    }
}
