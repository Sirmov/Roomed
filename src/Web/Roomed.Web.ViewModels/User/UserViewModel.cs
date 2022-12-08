namespace Roomed.Web.ViewModels.User
{
    using Roomed.Data.Models;
    using Roomed.Services.Data.Dtos.User;
    using Roomed.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>, IMapTo<UserDto>
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
