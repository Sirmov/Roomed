namespace Roomed.Web.ViewModels.Room
{
    using Roomed.Web.ViewModels.RoomType;

    public class RoomViewModel
    {
        public int TypeId { get; set; }

        public string Number { get; set; } = null!;

        public RoomTypeViewModel Type { get; set; } = null!;
    }
}
