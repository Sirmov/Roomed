namespace Roomed.Web.ViewModels.RoomType
{
    using Roomed.Services.Data.Dtos.RoomType;
    using Roomed.Services.Mapping;

    public class RoomTypeViewModel : IMapFrom<RoomTypeDto>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
