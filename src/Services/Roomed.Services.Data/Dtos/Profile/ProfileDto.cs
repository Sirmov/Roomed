namespace Roomed.Services.Data.Dtos.Profile
{
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Mapping;

    public class ProfileDto : IMapFrom<Roomed.Data.Models.Profile>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public DateOnly? Birthdate { get; set; }

        public Gender? Gender { get; set; }

        public string? Nationality { get; set; }

        public string? Address { get; set; }
    }
}
