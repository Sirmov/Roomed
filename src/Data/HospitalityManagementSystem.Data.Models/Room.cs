namespace HospitalityManagementSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HospitalityManagementSystem.Common;
    using HospitalityManagmentSystem.Data.Common.Models;

    public class Room : BaseDeletableModel<int>
    {
        public RoomType Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(GlobalConstants.RoomNumberMaxLength)]
        public string Number { get; set; }
    }
}
