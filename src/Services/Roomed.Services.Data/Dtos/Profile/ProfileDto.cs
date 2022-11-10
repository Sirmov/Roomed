namespace Roomed.Services.Data.Dtos.Profile
{
    using Roomed.Services.Mapping;

    public class ProfileDto : IMapFrom<Roomed.Data.Models.Profile>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }
}
