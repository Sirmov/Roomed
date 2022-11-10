﻿namespace Roomed.Web.ViewModels.Room
{
    using Roomed.Services.Data.Dtos.Room;
    using Roomed.Services.Mapping;
    using Roomed.Web.ViewModels.RoomType;

    public class RoomViewModel : IMapFrom<RoomDto>
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string Number { get; set; } = null!;

        public virtual RoomTypeViewModel Type { get; set; } = null!;
    }
}
