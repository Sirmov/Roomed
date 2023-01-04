// |-----------------------------------------------------------------------------------------------------|
// <copyright file="Program.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

using System.Reflection;

using AutoMapper;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

using static Roomed.Common.Constants.DataConstants.ApplicationUser;

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

        // Session configuration
        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;

            options.IOTimeout = TimeSpan.FromSeconds(15);

            options.IdleTimeout = TimeSpan.FromMinutes(10);
        });

        // Add response caching
        services.AddResponseCaching();

        // Add policies
        services.AddAuthorization(options =>
        {
            options.AddPolicy("FrontOffice", policy =>
                policy.RequireRole("Receptionist", "HotelsManager"));

            options.AddPolicy("Administration", policy =>
                policy.RequireRole("Administrator"));
        });

        // Change redirect links
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

        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        // Add filters and cache profiles
        services.AddControllersWithViews()
            .AddCookieTempDataProvider()
            .AddMvcOptions(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();

                options.CacheProfiles.Add("ErrorPage", new CacheProfile()
                {
                    Duration = TimeSpan.FromMinutes(15).Seconds,
                    Location = ResponseCacheLocation.Any,
                    NoStore = false,
                });
            });
    }

    private static async Task ConfigurePipelineAsync(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await ApplicationDbContextSeeder.SeedAsync(dbContext, serviceScope.ServiceProvider);
            }
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();
        app.UseResponseCaching();

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
