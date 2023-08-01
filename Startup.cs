using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ZimskaLiga.Controllers;
using ZimskaLiga.Models;
using ZimskaLiga.repository;
using ZimskaLiga.ViewModels;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddControllersWithViews();
        services.AddDbContext<LoginDbcontext>(conn => conn.UseSqlServer(Configuration.GetConnectionString("connectionstr")));
        services.AddScoped<ILogin, AuthenticateLogin>();
        services.AddMvc();
        services.AddControllersWithViews();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        //app.MapGet("/Layout", () => );
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();
        app.MapDefaultControllerRoute();
        app.Run();
    }
}