namespace Roomed.Services.Data.Dtos.Room
{
    using Roomed.Services.Data.Dtos.RoomType;
    using Roomed.Services.Mapping;

    public class RoomDto : IMapFrom<Roomed.Data.Models.Room>
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string Number { get; set; } = null!;

        public virtual RoomTypeDto Type { get; set; } = null!;
    }
}
