namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Data.Common.Models;

    using static Roomed.Common.DataConstants.Room;

    public class Room : BaseDeletableModel<int>
    {
        public RoomType Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(RoomNumberMaxLength)]
        public string Number { get; set; }
    }
}
