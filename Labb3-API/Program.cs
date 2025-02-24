
using Labb3_API.Data; 
using Labb3_API.Models;

namespace Labb3_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            TechnologyData db = new TechnologyData("CVDB", configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
  
            app.UseSwagger();
            app.UseSwaggerUI();
            

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseAuthorization();

            app.MapPost("/technology", async (Technology technology) =>
            {
                var CVDB = await db.AddTechnology("Technologies", technology);
                return Results.Ok(CVDB);
            });

            app.MapPut("/technology/{id}", async (string id, string name, int yearsOfExperience, string skillLevel) =>
            {
                var CVDB = await db.UpdateTechnology("Technologies", id, name, yearsOfExperience, skillLevel);
                return Results.Ok(CVDB);
            });

            app.MapGet("/technologies", async () =>
            {
                var CVDB = await db.GetAllTechnologies("Technologies");
                return Results.Ok(CVDB);
            });

            app.MapGet("/technology/{id}", async (string id) =>
            {
                var CVDB = await db.GetTechnologyById("Technologies", id);
                return Results.Ok(CVDB);
            });

            app.MapDelete("/technology/{id}", async (string id) =>
            {
                var CVDB = await db.DeleteTechnology("Technologies", id);
                return Results.Ok(CVDB);
            });

            app.Run();
        }
    }
}
