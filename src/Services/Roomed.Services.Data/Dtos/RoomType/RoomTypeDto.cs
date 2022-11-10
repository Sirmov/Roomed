namespace Roomed.Services.Data.Dtos.RoomType
{
    using Roomed.Services.Mapping;

    public class RoomTypeDto : IMapFrom<Roomed.Data.Models.RoomType>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
