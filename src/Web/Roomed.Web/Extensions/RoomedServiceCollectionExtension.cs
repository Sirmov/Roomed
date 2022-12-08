namespace Roomed.Web.Extensions
{
    using Roomed.Services.Data;
    using Roomed.Services.Data.Contracts;

    /// <summary>
    /// Static class containing all <see cref="IServiceCollection"/> extension methods regarding application services.
    /// </summary>
    public static class RoomedServiceCollectionExtension
    {
        /// <summary>
        /// This method registers all data services in the application DI container as scoped.
        /// </summary>
        /// <param name="services">The implementation of <see cref="IServiceCollection"/>.</param>
        /// <returns>The service collection with all data services added.</returns>
        public static IServiceCollection AddRoomedDataServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUsersService<,>), typeof(UsersService<,>));
            services.AddScoped<IReservationsService, ReservationsService>();
            services.AddScoped<IIdentityDocumentsService, IdentityDocumentsService>();
            services.AddScoped<IProfilesService, ProfilesService>();
            services.AddScoped<IRoomsService, RoomsService>();

            return services;
        }
    }
}
