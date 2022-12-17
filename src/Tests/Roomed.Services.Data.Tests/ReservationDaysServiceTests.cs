// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDaysServiceTests.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Tests
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Tests.Common;

    /// <summary>
    /// This class contains all unit tests for <see cref="ReservationDaysService"/>.
    /// </summary>
    [TestFixture]
    public class ReservationDaysServiceTests
    {
        private readonly ICollection<ReservationDay> profiles = new List<ReservationDay>()
        {
            new ReservationDay()
            {
                Id = Guid.Parse("c78a9611-9e28-4593-8c58-50a8bd6a1042"),
                ReservationId = Guid.Parse("a0368b88-05bb-48ff-83cb-0c1c6a323e4e"),
                Date = new DateOnly(2022, 8, 18),
            },
            new ReservationDay()
            {
                Id = Guid.Parse("3bb8c99c-305c-44be-ace1-d937b25bccc3"),
                ReservationId = Guid.Parse("a0368b88-05bb-48ff-83cb-0c1c6a323e4e"),
                Date = new DateOnly(2022, 8, 19),
            },
            new ReservationDay()
            {
                Id = Guid.Parse("1db7416c-48ce-46e1-b6ac-5b09f9296bb4"),
                ReservationId = Guid.Parse("a0368b88-05bb-48ff-83cb-0c1c6a323e4e"),
                Date = new DateOnly(2022, 8, 20),
            },
            new ReservationDay()
            {
                Id = Guid.Parse("31479f9e-badc-49b9-8a6e-1a12ed03c7b8"),
                ReservationId = Guid.Parse("a0368b88-05bb-48ff-83cb-0c1c6a323e4e"),
                Date = new DateOnly(2022, 8, 21),
            },
        }.AsReadOnly();

        private IMapper mapper;
        private IDeletableEntityRepository<ReservationDay, Guid> repository;
        private IRoomsService roomsService;
        private IReservationsService reservationsService;

        /// <summary>
        /// This method is called before every test.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        [SetUp]
        public async Task SetUp()
        {
            this.mapper = MapperMock.Instance;
            this.repository = DeletableEntityRepositoryMock<ReservationDay, Guid>.Instance;
            this.roomsService = RoomsServiceMock.Instance;
            this.reservationsService = ReservationsServiceMock.Instance;

            foreach (var item in this.profiles)
            {
                await this.repository.AddAsync(item);
            }
        }

        /// <summary>
        /// This method is called after every test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.repository.Dispose();
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationDaysService.GetAllAsync(Common.QueryOptions{Dtos.ReservationDay.ReservationDayDto}?)"/>
        /// returns all not deleted reservation days.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllAsync(QueryOptions<ReservationDayDto>? queryOptions = null)
        [Test]
        public async Task GetAllAsyncShouldReturnAllNotDeletedReservationDays()
        {
            // Arrange
            var service = new ReservationDaysService(this.repository, this.mapper);

            // Act
            var dtos = await service.GetAllAsync();

            // Assert
            var entities = this.repository.All(false, false).ToList();
            var dates = entities.Select(e => e.Date);

            Assert.That(dtos, Has.Exactly(entities.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(rt => !dates.Contains(rt.Date)), Is.False, "Entities are not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationDaysService.GetAsync(Guid, Common.QueryOptions{Dtos.ReservationDay.ReservationDayDto}?)"/>
        /// throws an exception when no reservation day with specified id exists.
        /// </summary>
        /// <param name="id">The id of a non existing reservation day.</param>
        // GetAsync(Guid id, QueryOptions<ReservationDayDto>? queryOptions = null)
        [Test]
        [TestCase("c717270d-6e11-46c3-b68d-15a1e6ccc439")]
        public void GetAsyncShouldThrowWhenReservationDayDoesNotExist(string id)
        {
            // Arrange
            var service = new ReservationDaysService(this.repository, this.mapper);
            var guid = Guid.Parse(id);

            // Act
            var code = async () => await service.GetAsync(guid);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationDaysService.GetAsync(Guid, Common.QueryOptions{Dtos.ReservationDay.ReservationDayDto}?)"/>
        /// returns the correct reservation day by a given id.
        /// </summary>
        /// <param name="id">The id of a existing reservation day.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAsync(Guid id, QueryOptions<ReservationDayDto>? queryOptions = null)
        [Test]
        [TestCase("3bb8c99c-305c-44be-ace1-d937b25bccc3")]
        public async Task GetAsyncShoulReturnExactResevationDay(string id)
        {
            // Arrange
            var service = new ReservationDaysService(this.repository, this.mapper);
            var guid = Guid.Parse(id);

            // Act
            var dto = await service.GetAsync(guid);

            // Assert
            var entity = this.repository.Find(guid);

            Assert.That(dto.Id, Is.EqualTo(entity.Id), "Entity's id is not correct.");
            Assert.That(dto.Date, Is.EqualTo(entity.Date), "Entity's date is not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationDaysService.GetAllForDateAsync(DateOnly, Common.QueryOptions{Dtos.ReservationDay.ReservationDayDto}?)"/>
        /// returns only the reservation days on a given date.
        /// </summary>
        /// <param name="year">The year of the date.</param>
        /// <param name="month">The month of the date.</param>
        /// <param name="day">The day of the date.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllForDateAsync(DateOnly date, QueryOptions<ReservationDayDto>? queryOptions = null)
        [Test]
        [TestCase(2022, 8, 20)]
        public async Task GetAllForDateAsyncShouldReturnCorrectReservationDays(int year, int month, int day)
        {
            // Arrange
            var service = new ReservationDaysService(this.repository, this.mapper);
            var date = new DateOnly(year, month, day);

            // Act
            var dtos = await service.GetAllForDateAsync(date);

            // Assert
            Assert.That(dtos.All(rd => rd.Date == date), Is.True, "Incorrect entities.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationDaysService.GetAllForPeriodAsync(DateOnly, DateOnly, Common.QueryOptions{Dtos.ReservationDay.ReservationDayDto}?)"/>
        /// returns only the reservation days for a given period.
        /// </summary>
        /// <param name="starYear">The year of the start of the period.</param>
        /// <param name="startMonth">The month of the start of the period.</param>
        /// <param name="startDay">The day of the start of the period.</param>
        /// <param name="endYear">The year of the end of the period.</param>
        /// <param name="endMonth">The month of the end of the period.</param>
        /// <param name="endDay">The day of the end of the period.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllForPeriodAsync(DateOnly startDate, DateOnly endDate, QueryOptions<ReservationDayDto>? queryOptions = null)
        [Test]
        [TestCase(2022, 8, 10, 2022, 8, 30)]
        public async Task GetAllForPeriodAsyncShouldReturnCorrectReservationDays(
            int starYear, int startMonth, int startDay,
            int endYear, int endMonth, int endDay)
        {
            // Arrange
            var service = new ReservationDaysService(this.repository, this.mapper);
            var startDate = new DateOnly(starYear, startMonth, startDay);
            var endDate = new DateOnly(endYear, endMonth, endDay);

            // Act
            var dtos = await service.GetAllForPeriodAsync(startDate, endDate);

            // Assert
            Assert.That(dtos.All(rd => rd.Date >= startDate && rd.Date <= endDate), "Incorrect entities.");
        }

        ///// <summary>
        ///// This test checks whether <see cref="ReservationDaysService.CreateForReservationAsync(Reservation, int)"/>
        ///// throws an exception when the reservation does not exist.
        ///// </summary>
        ///// <param name="reservationId">The id of a non existing reservation.</param>
        ///// <param name="roomId">The id of a existing room.</param>
        //// CreateForReservationAsync(Reservation reservation, int roomId)
        //[Test]
        //[TestCase("4f43e9ba-e187-4831-b723-55c64397b84c", 1)]
        //public void CreateForReservationAsyncShouldThrowWhenReservationDoesNotExist(string reservationId, int roomId)
        //{
        //    // Arrange
        //    var service = new ReservationDaysService(this.repository, this.mapper);

        //    // Act
        //    var reservation = new Reservation() { Id = Guid.Parse(reservationId) };

        //    var code = async () => await service.CreateForReservationAsync(reservation, roomId);

        //    // Assert
        //    var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
        //    Assert.That(exception.Message, Is.EqualTo("Reservation cannot be found."), "Exception message is not correct.");
        //}

        ///// <summary>
        ///// This test checks whether <see cref="ReservationDaysService.CreateForReservationAsync(Reservation, int)"/>
        ///// throws an exception when no room exists by a given id.
        ///// </summary>
        ///// <param name="reservationId">The id of a existing reservation.</param>
        ///// <param name="roomId">The id of a non existing room.</param>
        //// CreateForReservationAsync(Reservation reservation, int roomId)
        //[Test]
        //[TestCase("a0368b88-05bb-48ff-83cb-0c1c6a323e4e", 0)]
        //public void CreateForReservationAsyncShouldThrowWhenRoomDoesNotExist(string reservationId, int roomId)
        //{
        //    // Arrange
        //    var service = new ReservationDaysService(this.repository, this.mapper);

        //    // Act
        //    var reservation = new Reservation() { Id = Guid.Parse(reservationId) };

        //    var code = async () => await service.CreateForReservationAsync(reservation, roomId);

        //    // Assert
        //    var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
        //    Assert.That(exception.Message, Is.EqualTo("Room cannot be found."), "Exception message is not correct.");
        //}

        /// <summary>
        /// This test checks whether <see cref="ReservationDaysService.CreateForReservationAsync(Reservation, int)"/>
        /// throws an exception when there are reservation days for a reservation.
        /// </summary>
        /// <param name="reservationId">The id of a reservation which already has reservation days.</param>
        /// <param name="roomId">The id of a existing room.</param>
        // CreateForReservationAsync(Reservation reservation, int roomId)
        [Test]
        [TestCase("a0368b88-05bb-48ff-83cb-0c1c6a323e4e", 4)]
        public void CreateForReservationAsyncShouldThrowWhenReservationDaysExist(string reservationId, int roomId)
        {
            // Arrange
            var service = new ReservationDaysService(this.repository, this.mapper);

            // Act
            var reservation = new Reservation() { Id = Guid.Parse(reservationId) };

            var code = async () => await service.CreateForReservationAsync(reservation, roomId);

            // Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
            Assert.That(exception.Message, Is.EqualTo("There are reservation days for this reservation already."), "Exception message is not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationDaysService.CreateForReservationAsync(Reservation, int)"/>
        /// creates the correct reservation days for a given reservation and room id.
        /// </summary>
        /// <param name="reservationId">The id of an existing reservation not having any reservation days.</param>
        /// <param name="roomId">The id of a existing room.</param>
        /// <param name="arrivalYear">The year of the date of arrival.</param>
        /// <param name="arrivalMonth">The month of the date of arrival.</param>
        /// <param name="arrivalDay">The day of the date of arrival.</param>
        /// <param name="departureYear">The year of the date of departure.</param>
        /// <param name="departureMonth">The month of the date of departure.</param>
        /// <param name="departureDay">The day of the date of departure.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // CreateForReservationAsync(Reservation reservation, int roomId)
        [Test]
        [TestCase("33fec2d6-a85c-40f8-8738-848c68bc2ac8", 3, 2022, 7, 7, 2022, 7, 9)]
        public async Task CreateForReservationAsyncShouldCreateCorrectReservationDays(
            string reservationId, int roomId,
            int arrivalYear, int arrivalMonth, int arrivalDay,
            int departureYear, int departureMonth, int departureDay)
        {
            // Arrange
            var service = new ReservationDaysService(this.repository, this.mapper);
            var reservationGuid = Guid.Parse(reservationId);
            var arrivalDate = new DateOnly(arrivalYear, arrivalMonth, arrivalDay);
            var departureDate = new DateOnly(departureYear, departureMonth, departureDay);

            // Act
            var reservation = new Reservation()
            {
                Id = reservationGuid,
                ArrivalDate = arrivalDate,
                DepartureDate = departureDate,
            };

            await service.CreateForReservationAsync(reservation, roomId);

            // Assert
            var reservationDays = await this.repository
                .All()
                .Where(rd => rd.Date >= arrivalDate && rd.Date <= departureDate)
                .ToListAsync();
            int reservationLength = (departureDate.DayNumber - arrivalDate.DayNumber) + 1;

            Assert.That(reservationDays, Has.Exactly(reservationLength).Items, "Reservation days count is not correct.");
            Assert.IsTrue(reservationDays.All(rd => rd.ReservationId == reservationGuid), "Reservation days are not correct.");
        }
    }
}
