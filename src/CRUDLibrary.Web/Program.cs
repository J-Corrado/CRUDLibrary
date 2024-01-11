using CRUDLibrary.Domain.Extensions;
using System.Text.Json.Serialization;
using CRUDLibrary.Data;



internal class Program
{
    public static void Main(string[] args)
    {
        
    

        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContexts(builder.Configuration);
        builder.Services.AddServices();
        
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddControllers().AddJsonOptions(x =>
                        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        
        
        
        builder.Services.AddMemoryCache();
        builder.Services.AddSession();
        
        
        var app = builder.Build();
        
        if (args.Length == 1 && args[0].ToLower() == "seeddata")
        {
            Seed.SeedData(app);
        }
        
        
        
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        
        app.UseRouting();
        
        app.UseAuthorization();
        
        
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        
        app.Run();

    }
}