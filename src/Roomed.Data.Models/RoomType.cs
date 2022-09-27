namespace Roomed.Data.Models
{
    using Roomed.Data.Common.Models;

    public class RoomType : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
