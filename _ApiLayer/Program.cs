using _LogicLayer.Logics;
using _LogicLayer.Processes;
using _LogicLayer.Processes.Process1;
using _Providers.DatabaseProviders.MongoDb;
using Library._Api.ApiExceptionManager;
using Library.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace _ApiLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ApiExceptionManager>();

            // Récupère les informations dans "appsettings.json"
            builder.Services.Configure<SettingsServiceMongoDb>(builder.Configuration.GetSection("MongoDatabase"));
            builder.Services.Configure<SettingsApi>(builder.Configuration.GetSection("Callers"));

            // Inscrit la couche PROVIDER dans l'injection de dépendance
            builder.Services.AddTransient<ServiceMongoDatabase>();

            // Inscrit la couche LOGIC dans l'injection de dépendance
            builder.Services.AddTransient<FamilyLogic>();
            builder.Services.AddTransient<ParentLogic>();
            builder.Services.AddTransient<ChildLogic>();
            builder.Services.AddTransient<ProcessLogic>();

            // Inscrit CHAQUE Process et CHAQUE Steps
            builder.Services.AddSingleton<ProcessHandler>();
            builder.Services.AddSingleton<ClientProcess1>();
            builder.Services.AddSingleton<Process1Step1>();
            builder.Services.AddSingleton<Process1Step2>();

            // Set up MongoDB conventions
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}