using Blazor.Controller.Modal;
using Blazor.Provider.AddressProvider;
using Blazor.Provider.Api;
using Blazor.Provider.Api.CallApiProviderBase;
using Blazor.Provider.Api.MES;
using Blazor.Provider.BlazorExceptionManager;
using Blazor.Provider.LoraineProvider;
using Blazored.Modal;
using Common.Models.MES;
using MudBlazor.Services;
using MudExtensions.Services;

namespace Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            
            builder.Services.AddMudServices();
            builder.Services.AddMudExtensions();
            builder.Services.AddBlazoredModal();

            builder.Services.AddScoped<ModalController>();

            builder.Services.AddScoped<BlazorExceptionManager>();
            
            // R�cup�re la route principale de l'api dans "appsettings.json"
            builder.Services.Configure<SettingsCallApi>(builder.Configuration.GetSection("Api"));

            // Permet � FamilleCallApi d'apeller l'api
            builder.Services.AddHttpClient<FamilyProvider>();
            builder.Services.AddHttpClient<ParentProvider>();
            builder.Services.AddHttpClient<ChildProvider>();
            builder.Services.AddHttpClient<ApiAddressProvider>();
            builder.Services.AddHttpClient<LorraineHipseaumeProvider>();
            builder.Services.AddHttpClient<LorraineIpsumProvider>();
            builder.Services.AddHttpClient<ProcessProvider>();

            builder.Services.AddHttpClient<ICallApi<Article>, ArticleProvider>();
            builder.Services.AddHttpClient<ICallApi<Gamme>, GammeProvider>();
            builder.Services.AddHttpClient<ICallApi<Etape>, EtapeProvider>();
            builder.Services.AddHttpClient<ICallApi<GammeEtape>, GammeEtapeProvider>();
            builder.Services.AddHttpClient<ICallApi<OrdreFabrication>, OrdreFabricationProvider>();


            builder.Services.AddControllers().AddNewtonsoftJson();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.UseAuthentication(); ;

            app.Run();
        }
    }
}