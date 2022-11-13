namespace Roomed.Web.ViewModels.RoomType
{
    using Roomed.Services.Data.Dtos.RoomType;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.RoomType"/> view model.
    /// </summary>
    public class RoomTypeViewModel : IMapFrom<RoomTypeDto>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
