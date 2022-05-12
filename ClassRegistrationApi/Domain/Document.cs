using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClassRegistrationApi.Domain;

public class Registration
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    [BsonElement("registration")]
    public RegistrationDetails Details { get; set; } = new RegistrationDetails();
}

public class RegistrationDetails
{
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
    [BsonElement("dateOfCourse")]
    public DateTime DateOfCourse { get; set; }
    [BsonElement("course")]
    public string Course { get; set; } = string.Empty;
}
