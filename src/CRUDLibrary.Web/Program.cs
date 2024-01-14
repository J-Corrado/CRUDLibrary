using CRUDLibrary.Domain.Extensions;
using CRUDLibrary.Data;



internal class Program
{
    public static void Main(string[] args)
    {
        
    

        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContexts(builder.Configuration);
        builder.Services.AddServices();
        
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
        {
            jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
        
        
        builder.Services.AddMemoryCache();
        builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
        builder.Services.AddSession(options =>
        {
            // Set session timeout
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        
        
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
        app.UseSession();
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