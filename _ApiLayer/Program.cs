using _ApiLayer.Controllers.ApiControllerBase;
using _LogicLayer.Logics;
using _LogicLayer.Logics.LogicBase;
using _LogicLayer.Logics.MES;
using _LogicLayer.Processes;
using _LogicLayer.Processes.Process1;
using _Providers.DatabaseProviders;
using _Providers.DatabaseProviders.MongoDb;
using _Providers.DatabaseProviders.SQLServer;
using Common.Models;
using Common.Models.Business;
using Common.Models.MES;
using Common.Models.Processes;
using Microsoft.EntityFrameworkCore;
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

            builder.Services.AddTransient<ApiExceptionManager.ApiExceptionManager>();

            // Récupère les informations dans "appsettings.json"
            builder.Services.Configure<SettingsServiceMongoDb>(builder.Configuration.GetSection("MongoDatabase"));
            builder.Services.Configure<SettingsApi>(builder.Configuration.GetSection("Callers"));
            builder.Services.AddDbContext<SQLServerContext<Entity>>(options => options.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False"));
            
            // Inscrit les objets de la couche PROVIDER dans l'injection de dépendance
            builder.Services.AddTransient<IApiServiceDatabase ,ServiceMongoDatabase>();

            // Inscrit les objets de la couche LOGIC dans l'injection de dépendance
            builder.Services.AddTransient<ILogic<Family>, FamilyLogic>();
            builder.Services.AddTransient<ILogic<Parent>, ParentLogic>();
            builder.Services.AddTransient<ILogic<Child>, ChildLogic>();
            builder.Services.AddTransient<ILogic<Process>, ProcessLogic>();

            builder.Services.AddTransient<ILogic<ManufacturedArticle>, ManufacturedArticleLogic>();
            builder.Services.AddTransient<ILogic<PurchasedArticle>, PurchasedArticleLogic>();
            builder.Services.AddTransient<ILogic<Of>, OfLogic>();
            builder.Services.AddTransient<ILogic<Ope>, OpeLogic>();
            builder.Services.AddTransient<ILogic<Gamme>, GammeLogic>();

            // Inscrit CHAQUE Process et CHAQUE Steps dans l'injection de dépendance
            builder.Services.AddTransient<ProcessHandler>();
            builder.Services.AddTransient<ClientProcess1>();
            builder.Services.AddTransient<Process1Step1>();
            builder.Services.AddTransient<Process1Step2>();

            // Set up MongoDB pour inscrire les valeurs des enum en BDD
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