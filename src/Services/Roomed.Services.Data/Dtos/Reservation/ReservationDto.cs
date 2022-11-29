﻿namespace Roomed.Services.Data.Dtos.Reservation
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common.ValidationAttributes;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Data.Dtos.ReservationDay;
    using Roomed.Services.Data.Dtos.RoomType;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.Reservation;

    public class ReservationDto : IMapFrom<Roomed.Data.Models.Reservation>
    {
        public Guid? Id { get; set; }

        [Required]
        public Guid ReservationHolderId { get; set; }

        [Required]
        [BeforeDate(nameof(DepartureDate))]
        public DateOnly ArrivalDate { get; set; }

        [Required]
        [AfterDate(nameof(ArrivalDate))]
        public DateOnly DepartureDate { get; set; }

        [Required]
        [EnumDataType(typeof(ReservationStatus))]
        public ReservationStatus Status { get; set; }

        [Required]
        public int RoomTypeId { get; set; }

        [Required]
        [Range(0, AdultsMaxCount)]
        public int Adults { get; set; }

        [Required]
        [Range(0, TeenagersMaxCount)]
        public int Teenagers { get; set; }

        [Required]
        [Range(0, ChildrenMaxCount)]
        public int Children { get; set; }

        public DetailedProfileDto ReservationHolder { get; set; } = null!;

        public RoomTypeDto RoomType { get; set; } = null!;

        public ICollection<ReservationDayDto> ReservationDays { get; set; }
    }
}
