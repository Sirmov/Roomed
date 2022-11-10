namespace Roomed.Services.Data.Dtos.ProfileNote
{
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Mapping;

    public class ProfileNoteDto : IMapFrom<Roomed.Data.Models.ProfileNote>
    {
        public Guid Id { get; set; }

        public string Body { get; set; } = null!;

        public Guid ProfileId { get; set; }

        public ProfileDto Profile { get; set; } = null!;
    }
}
