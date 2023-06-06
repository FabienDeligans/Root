using Api.Logics;
using Api.Processes;
using Api.Processes.Process1;
using Api.Services.MongoDb;
using Library.Api.ApiExceptionManager;
using Library.Settings;

namespace Api
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

            // Inscrit les couches Logic et Service dans l'injection de dépendance
            builder.Services.AddTransient<ServiceMongoDatabase>();
            builder.Services.AddTransient<FamilyLogic>();
            builder.Services.AddTransient<ParentLogic>();
            builder.Services.AddTransient<ChildLogic>();
            builder.Services.AddTransient<ProcessLogic>();

            // Inscrit CHAQUE Process et CHAQUE Steps
            builder.Services.AddSingleton<ProcessHandler>(); 
            builder.Services.AddSingleton<ClientProcess1>(); 
            builder.Services.AddTransient<Process1Step1>(); 
            builder.Services.AddTransient<Process1Step2>();


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