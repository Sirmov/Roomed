namespace HospitalityManagementSystem.Data.Models
{
    using HospitalityManagmentSystem.Data.Common.Models;

    public class RoomType : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
