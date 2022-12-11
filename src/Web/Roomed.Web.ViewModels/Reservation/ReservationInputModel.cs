// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationInputModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.Reservation
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common.ValidationAttributes;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.Reservation;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.Reservation"/> input model.
    /// </summary>
    public class ReservationInputModel : IMapFrom<ReservationDto>,  IMapTo<ReservationDto>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid? Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.ReservationHolderId"/>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Reservation holder")]
        public Guid ReservationHolderId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.RoomTypeId"/>
        [Required(AllowEmptyStrings = false)]
        public int RoomTypeId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.ArrivalDate"/>
        [Required(AllowEmptyStrings = false)]
        [BeforeDate(nameof(DepartureDate))]
        [DataType(DataType.Date)]
        [Display(Name = "Arrival date")]
        public DateOnly ArrivalDate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.DepartureDate"/>
        [Required(AllowEmptyStrings = false)]
        [AfterDate(nameof(ArrivalDate))]
        [DataType(DataType.Date)]
        [Display(Name = "Departure date")]
        public DateOnly DepartureDate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.Adults"/>
        [Required]
        [Range(1, AdultsMaxCount)]
        public int Adults { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.Teenagers"/>
        [Required]
        [Range(0, TeenagersMaxCount)]
        public int Teenagers { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.Children"/>
        [Required]
        [Range(0, ChildrenMaxCount)]
        public int Children { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.Status"/>
        [EnumDataType(typeof(ReservationStatus))]
        public ReservationStatus? Status { get; set; }
    }
}
