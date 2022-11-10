namespace Roomed.Web.ViewModels.Profile
{
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Mapping;

    public class ProfileViewModel : IMapFrom<ProfileDto>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }
}
