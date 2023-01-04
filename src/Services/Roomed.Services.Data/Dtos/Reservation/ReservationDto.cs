// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDto.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Dtos.Reservation
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common.ValidationAttributes;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Data.Dtos.ReservationDay;
    using Roomed.Services.Data.Dtos.RoomType;
    using Roomed.Services.Mapping;

    using static Roomed.Common.Constants.DataConstants.Reservation;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.Reservation"/> data transfer object.
    /// </summary>
    public class ReservationDto : IMapFrom<Roomed.Data.Models.Reservation>, IMapTo<Roomed.Data.Models.Reservation>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid? Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.ReservationHolderId"/>
        [Required]
        public Guid ReservationHolderId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.ArrivalDate"/>
        [Required]
        [BeforeDate(typeof(DateOnly), nameof(DepartureDate))]
        public DateOnly ArrivalDate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.DepartureDate"/>
        [Required]
        [AfterDate(typeof(DateOnly), nameof(ArrivalDate))]
        public DateOnly DepartureDate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.Status"/>
        [Required]
        [EnumDataType(typeof(ReservationStatus))]
        public ReservationStatus Status { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.RoomTypeId"/>
        [Required]
        public int RoomTypeId { get; set; }

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

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.ReservationHolder"/>
        public DetailedProfileDto ReservationHolder { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.RoomType"/>
        public RoomTypeDto RoomType { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.ReservationDays"/>
        public ICollection<ReservationDayDto> ReservationDays { get; set; } = new HashSet<ReservationDayDto>();
    }
}
