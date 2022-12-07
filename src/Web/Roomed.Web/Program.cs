using System.Reflection;

using AutoMapper;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Roomed.Data;
using Roomed.Data.Common.Repositories;
using Roomed.Data.Models;
using Roomed.Data.Repositories;
using Roomed.Data.Seeding.Seeders;
using Roomed.Services.Data.Dtos.Reservation;
using Roomed.Services.Mapping;
using Roomed.Web.Extensions;
using Roomed.Web.ViewModels;

using static Roomed.Common.DataConstants.ApplicationUser;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configure application pipeline
        await ConfigurePipelineAsync(app);

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
            options.SignIn.RequireConfirmedAccount = configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");

            options.Lockout.MaxFailedAccessAttempts = configuration.GetValue<int>("Identity:Lockout:MaxFailedAccessAttempts");
            options.Lockout.DefaultLockoutTimeSpan = configuration.GetValue<TimeSpan>("Identity:Lockout:MaxFailedAccessAttempts");

            options.Password.RequireNonAlphanumeric = configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
            options.Password.RequireDigit = configuration.GetValue<bool>("Identity:Password:RequireDigit");
            options.Password.RequireUppercase = configuration.GetValue<bool>("Identity:Password:RequireUppercase");
            options.Password.RequiredLength = PasswordMinLength;
        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("FrontOffice", policy =>
                policy.RequireRole("Receptionist", "HotelsManager"));

            options.AddPolicy("Administration", policy =>
                policy.RequireRole("Administrator"));
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/User/Login";
            options.LogoutPath = "/User/Logout";
            options.AccessDeniedPath = "/User/AccessDenied";
        });

        // AutoMapper configuration
        var asseblies = new Assembly[]
        {
            typeof(ErrorViewModel).GetTypeInfo().Assembly,
            typeof(ReservationDto).GetTypeInfo().Assembly,
        };
        AutoMapperConfig.RegisterMappings(asseblies);
        IMapper mapper = AutoMapperConfig.MapperInstance;
        services.AddSingleton<IMapper>(mapper);

        // HtmlSanitizer configuration
        IHtmlSanitizer htmlSanitizer = new HtmlSanitizer(new HtmlSanitizerOptions() { AllowedTags = { } });
        services.AddSingleton<IHtmlSanitizer>(htmlSanitizer);

        // Register data repositories
        services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
        services.AddScoped(typeof(IDeletableEntityRepository<,>), typeof(EfDeletableEntityRepository<,>));

        // Register data services
        services.AddRoomedDataServices();

        // Add DateOnly and TimeOnly support
        services.AddDateOnlyTimeOnlyStringConverters();

        services.AddControllersWithViews()
            .AddMvcOptions(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
    }

    private static async Task ConfigurePipelineAsync(WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await ApplicationDbContextSeeder.SeedAsync(dbContext, serviceScope.ServiceProvider);
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
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

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.MapRazorPages();
    }
}
