using MongoDB.Bson.Serialization.Attributes;

namespace Labb3_API.Models
{
    public class Technology
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public string SkillLevel { get; set; }
    }
}
