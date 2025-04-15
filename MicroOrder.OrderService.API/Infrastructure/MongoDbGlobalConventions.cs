using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace MicroOrder.OrderService.API.Infrastructure;

public static class MongoDbGlobalConventions
{
    public static void RegisterGlobalConventions()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        var conventionPack = new ConventionPack
        {
            new EnumRepresentationConvention(BsonType.String),
            new CamelCaseElementNameConvention()
        };

        ConventionRegistry.Register(nameof(MongoDbGlobalConventions), conventionPack, _ => true);
        MongoClassConventions.RegisterClassConventions();
    }
}
