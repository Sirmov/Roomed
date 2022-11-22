namespace Roomed.Web.ViewModels.Profile
{
    using AutoMapper.Configuration.Annotations;
    using Roomed.Services.Mapping;

    public class ProfileViewModel : IMapFrom<DetailedProfileViewModel>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        [Ignore]
        public string FullName
        {
            get => $"{this.FirstName} {(this.MiddleName == null ? string.Empty : $"{this.MiddleName} ")}{this.LastName}";
        }
    }
}
