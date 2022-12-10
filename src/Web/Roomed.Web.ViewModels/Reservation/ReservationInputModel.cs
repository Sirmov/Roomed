﻿// <copyright file="ReservationInputModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Roomed.Web.ViewModels.Reservation
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common.ValidationAttributes;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.Reservation;

    public class ReservationInputModel : IMapFrom<ReservationDto>,  IMapTo<ReservationDto>
    {
        public Guid? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Reservation holder")]
        public Guid ReservationHolderId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int RoomTypeId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [BeforeDate(nameof(DepartureDate))]
        [DataType(DataType.Date)]
        [Display(Name = "Arrival date")]
        public DateOnly ArrivalDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        [AfterDate(nameof(ArrivalDate))]
        [DataType(DataType.Date)]
        [Display(Name = "Departure date")]
        public DateOnly DepartureDate { get; set; }

        [Required]
        [Range(1, AdultsMaxCount)]
        public int Adults { get; set; }

        [Required]
        [Range(0, TeenagersMaxCount)]
        public int Teenagers { get; set; }

        [Required]
        [Range(0, ChildrenMaxCount)]
        public int Children { get; set; }

        [EnumDataType(typeof(ReservationStatus))]
        public ReservationStatus? Status { get; set; }
    }
}
