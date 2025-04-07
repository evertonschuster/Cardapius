using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hexata.BI.Application.Dtos;

public class LocalizationDto
{
    public required string Id { get; set; }

    [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
    public double Latitude { get; set; }

    [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
    public double Longitude { get; set; }

    public required string Precision { get; set; }

    public required string Provider { get; set; }
}
