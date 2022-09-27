namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common;
    using Roomed.Data.Common.Models;

    public class Room : BaseDeletableModel<int>
    {
        public RoomType Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(GlobalConstants.RoomNumberMaxLength)]
        public string Number { get; set; }
    }
}
