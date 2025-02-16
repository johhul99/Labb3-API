using Labb3_API.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Labb3_API.Data
{
    public class TechnologyData
    {
        private IMongoDatabase db;

        public TechnologyData(string database, IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("DefaultConnection"));
            db = client.GetDatabase(database);
        }

        public async Task<List<Technology>> AddTechnology(string table, Technology technology)
        {
            var collection = db.GetCollection<Technology>(table);
            await collection.InsertOneAsync(technology);  
            return collection.AsQueryable().ToList();
        }

        public async Task<List<Technology>> UpdateTechnology(string table, string id, string name, float yearsOfExperience, string skillLevel)
        {
            var collection = db.GetCollection<Technology>(table);
            var technology = await collection.Find(t => t.Id == id).FirstOrDefaultAsync();
            if(technology != null)
            {
                technology.Name = name;
                technology.YearsOfExperience = yearsOfExperience;
                technology.SkillLevel = skillLevel;
                
                await collection.ReplaceOneAsync(t => t.Id == id, technology);
            }

            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<List<Technology>> GetAllTechnologies(string table)
        {
            var collection = db.GetCollection<Technology>(table);
            var technologies = await collection.AsQueryable().ToListAsync();
            return technologies;
        }

        public async Task<Technology> GetTechnologyById(string table, string id)
        {
            var collection = db.GetCollection<Technology>(table);
            var technology = await collection.Find(t =>t.Id == id).FirstOrDefaultAsync();
            return technology;
        }

        public async Task<string> DeleteTechnology (string table, string id)
        {
            var collection = db.GetCollection<Technology>(table);
            var technology = await collection.DeleteOneAsync(t =>t.Id == id);
            return "Technology deleted!";
        }
    }
}
