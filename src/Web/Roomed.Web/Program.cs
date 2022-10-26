using System.Reflection;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Roomed.Data;
using Roomed.Data.Models;
using Roomed.Services.Data;
using Roomed.Services.Data.Contracts;
using Roomed.Services.Mapping;
using Roomed.Web.ViewModels;
using static Roomed.Common.DataConstants.ApplicationUser;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configure application pipeline
        ConfigurePipeline(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Database configuration
        string defaultConnectingString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(defaultConnectingString);
        });
        services.AddDatabaseDeveloperPageExceptionFilter();

        // Identity configuration
        services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = PasswordMinLength;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/User/Login";
        });

        // AutoMapper configuration
        AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        IMapper mapper = AutoMapperConfig.MapperInstance;
        services.AddSingleton(mapper);

        // Register data services
        services.AddTransient(typeof(IUsersService<>), typeof(UsersService<>));

        services.AddControllersWithViews();
    }

    private static void ConfigurePipeline(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();
    }
}