// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDaysServiceMock.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Tests.Common
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using Moq;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.ReservationDay;

    /// <summary>
    /// This class is a mock of <see cref="IReservationDaysService"/>.
    /// </summary>
    public class ReservationDaysServiceMock
    {
        private static readonly ICollection<ReservationDay> ReservationDays = new List<ReservationDay>()
        {
            new ReservationDay()
            {
                Id = Guid.Parse("73c42cc4-c40d-438d-9f27-82104534f46e"),
                Date = new DateOnly(2022, 6, 13),
                RoomId = 1,
                Room = new Room()
                    {
                        Id = 1,
                        Number = "10",
                        Type = new RoomType() { Id = 1, Name = "Double Room" },
                    },
                IsDeleted = false,
            },
            new ReservationDay()
            {
                Id = Guid.Parse("fa6699fa-b71f-4709-acbb-2ed9a49d00cf"),
                Date = new DateOnly(2022, 6, 14),
                RoomId = 1,
                Room = new Room()
                    {
                        Id = 1,
                        Number = "10",
                        Type = new RoomType() { Id = 1, Name = "Double Room" },
                    },
                IsDeleted = false,
            },
        }.AsReadOnly();

        private static IMapper mapper = MapperMock.Instance;
        private static IDeletableEntityRepository<ReservationDay, Guid> reservationDaysRepository = DeletableEntityRepositoryMock<ReservationDay, Guid>.Instance;

        private static bool isInitialized = false;

        /// <summary>
        /// Gets the <see cref="IReservationDaysService"/> instance of the mock.
        /// </summary>
        public static IReservationDaysService Instance
        {
            get
            {
                if (!isInitialized)
                {
                    foreach (var item in ReservationDays)
                    {
                        reservationDaysRepository.Add(item);
                    }
                }

                var mock = new Mock<IReservationDaysService>();

                mock.Setup(m => m.GetAllForDateAsync(It.IsAny<DateOnly>(), It.IsAny<QueryOptions<ReservationDayDto>?>()).Result)
                    .Returns((DateOnly date, QueryOptions<ReservationDayDto>? queryOptions) =>
                    {
                        var dtos = reservationDaysRepository
                            .All()
                            .Include(rd => rd.Room)
                            .ThenInclude(r => r.Type)
                            .Where(d => d.Date == date)
                            .ProjectTo<ReservationDayDto>(mapper.ConfigurationProvider)
                            .ToList();

                        return dtos;
                    });

                mock.Setup(m => m.GetAllForPeriodAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), It.IsAny<QueryOptions<ReservationDayDto>?>()).Result)
                    .Returns((DateOnly startDate, DateOnly endDate, QueryOptions<ReservationDayDto>? queryOptions) =>
                    {
                        var dtos = reservationDaysRepository
                            .All()
                            .Include(rd => rd.Room)
                            .ThenInclude(r => r.Type)
                            .Where(d => d.Date >= startDate && d.Date <= endDate)
                            .ProjectTo<ReservationDayDto>(mapper.ConfigurationProvider)
                            .ToList();

                        return dtos;
                    });

                var service = mock.Object;
                return service;
            }
        }
    }
}
