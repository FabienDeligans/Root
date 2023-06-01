using Blazor.Controller.Modal;
using Blazor.Provider;
using Blazored.Modal;
using Library.Blazor.BlazorExceptionManager;
using Library.Blazor.CallApiAddressProvider;
using Library.Blazor.CallApiLoraineProvider;
using Library.Settings;

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