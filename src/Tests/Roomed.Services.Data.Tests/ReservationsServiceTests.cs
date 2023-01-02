// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationsServiceTests.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Tests
{
    using AutoMapper;
    using Microsoft.Extensions.Options;
    using NUnit.Framework;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Tests.Common;

    /// <summary>
    /// This class contains all unit tests for <see cref="ReservationsService"/>.
    /// </summary>
    [TestFixture]
    public class ReservationsServiceTests
    {
        private readonly ICollection<Reservation> reservations = new List<Reservation>()
        {
            new Reservation()
            {
                Id = Guid.Parse("a51493f3-a2ac-44b7-b545-89ea1a78468e"),
                Status = ReservationStatus.Arriving,
                ArrivalDate = new DateOnly(2022, 1, 1),
                DepartureDate = new DateOnly(2022, 1, 2),
                ReservationDays = new List<ReservationDay>()
                {
                    new ReservationDay()
                    {
                        Id = Guid.Parse("ac90e6a1-7e19-446f-bd4e-0ce112e2bb48"),
                        ReservationId = Guid.Parse("a51493f3-a2ac-44b7-b545-89ea1a78468e"),
                        Date = new DateOnly(2022, 1, 1),
                    },
                    new ReservationDay()
                    {
                        Id = Guid.Parse("af0e210a-0845-41ae-8f43-df705aa42253"),
                        ReservationId = Guid.Parse("a51493f3-a2ac-44b7-b545-89ea1a78468e"),
                        Date = new DateOnly(2022, 1, 2),
                    },
                },
            },
            new Reservation()
            {
                Id = Guid.Parse("e20384d4-fbfd-40c2-ae34-e37c4c5e6330"),
                Status = ReservationStatus.InHouse,
                ArrivalDate = new DateOnly(2022, 2, 1),
                DepartureDate = new DateOnly(2022, 2, 2),
                ReservationDays = new List<ReservationDay>()
                {
                    new ReservationDay()
                    {
                        Id = Guid.Parse("774988ef-dbdb-4dc0-b7f0-480d6a43fac1"),
                        ReservationId = Guid.Parse("e20384d4-fbfd-40c2-ae34-e37c4c5e6330"),
                        Date = new DateOnly(2022, 2, 1),
                    },
                    new ReservationDay()
                    {
                        Id = Guid.Parse("6547edaa-5fbb-4a2a-8900-1c02739db882"),
                        ReservationId = Guid.Parse("e20384d4-fbfd-40c2-ae34-e37c4c5e6330"),
                        Date = new DateOnly(2022, 2, 2),
                    },
                },
            },
            new Reservation()
            {
                Id = Guid.Parse("033334e7-8fa3-4407-bdde-8fa6f3b8d175"),
                Status = ReservationStatus.Departuring,
                ArrivalDate = new DateOnly(2022, 3, 1),
                DepartureDate = new DateOnly(2022, 3, 2),
                ReservationDays = new List<ReservationDay>()
                {
                    new ReservationDay()
                    {
                        Id = Guid.Parse("080b955e-ed20-4618-8c39-24ce5cede0f7"),
                        ReservationId = Guid.Parse("033334e7-8fa3-4407-bdde-8fa6f3b8d175"),
                        Date = new DateOnly(2022, 3, 1),
                    },
                    new ReservationDay()
                    {
                        Id = Guid.Parse("209b1e36-3ba8-4250-a8a7-84c1bcf3c5f9"),
                        ReservationId = Guid.Parse("033334e7-8fa3-4407-bdde-8fa6f3b8d175"),
                        Date = new DateOnly(2022, 3, 2),
                    },
                },
            },
        }.AsReadOnly();

        private IMapper mapper;
        private IDeletableEntityRepository<Reservation, Guid> repository;
        private IRoomsService roomsService;
        private IReservationDaysService reservationDaysService;

        /// <summary>
        /// This method is called before every test.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        [SetUp]
        public async Task SetUp()
        {
            this.mapper = MapperMock.Instance;
            this.repository = DeletableEntityRepositoryMock<Reservation, Guid>.Instance;
            this.roomsService = RoomsServiceMock.Instance;
            this.reservationDaysService = ReservationDaysServiceMock.Instance;

            foreach (var item in this.reservations)
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

        ///// <summary>
        ///// This test checks whether <see cref="ReservationsService.GetAsync(Guid, Common.QueryOptions{Dtos.Reservation.ReservationDto}?)"/>
        ///// throws and exception when id is null.
        ///// </summary>
        //// GetAsync(Guid id, QueryOptions<ReservationDto>? queryOptions = null)
        // [Test]
        // public void GetAsyncShouldThrowWhenIdIsNull()
        // {
        //    // Arrange
        //    var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);

        //    // Act
        //    var code = async () => await service.GetAsync(null!);

        //    // Assert
        //    Assert.ThrowsAsync<ArgumentNullException>(async () => await code(), "Method should throw exception.");
        // }

        /// <summary>
        /// This test checks whether <see cref="ReservationsService.GetAsync(Guid, Common.QueryOptions{Dtos.Reservation.ReservationDto}?)"/>
        /// throws and exception no reservation with specified id can be found.
        /// </summary>
        /// <param name="id">The id of a non existing reservation.</param>
        // GetAsync(Guid id, QueryOptions<ReservationDto>? queryOptions = null)
        [Test]
        [TestCase("bd6d8567-b4e1-4c98-9420-7847bd1ddc4f")]
        public void GetAsyncShouldThrowWhenReservationDoesNotExist(string id)
        {
            // Arrange
            var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);
            var guid = Guid.Parse(id!);

            // Act
            var code = async () => await service.GetAsync(guid);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationsService.GetAsync(Guid, Common.QueryOptions{Dtos.Reservation.ReservationDto}?)"/>
        /// returns the correct reservation by a given id.
        /// </summary>
        /// <param name="id">The id of an existing reservation.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAsync(Guid id, QueryOptions<ReservationDto>? queryOptions = null)
        [Test]
        [TestCase("a51493f3-a2ac-44b7-b545-89ea1a78468e")]
        public async Task GetAsyncShouldReturnCorrectReservation(string id)
        {
            if (id == null)
            {
                Assert.Fail("Id should not be null.");
            }

            // Arrange
            var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);
            var guid = Guid.Parse(id!);

            // Act
            var dto = await service.GetAsync(guid);

            // Assert
            var reservation = this.repository.All().First(r => r.Id.ToString() == id);
            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(reservation.Id), "Entity's id is not correct.");
                Assert.That(dto.Status, Is.EqualTo(reservation.Status), "Entity's status is not correct.");
                Assert.That(dto.ArrivalDate, Is.EqualTo(reservation.ArrivalDate), "Entity's arrival date is not correct.");
                Assert.That(dto.DepartureDate, Is.EqualTo(reservation.DepartureDate), "Entity's departure date is not correct.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationsService.GetAllAsync(Common.QueryOptions{Dtos.Reservation.ReservationDto}?)"/>
        /// returns all not deleted reservations.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllAsync(QueryOptions<ReservationDto>? queryOptions = null)
        [Test]
        public async Task GetAllAsyncShouldReturnAllNotDeletedReservations()
        {
            // Arrange
            var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);

            // Act
            var dtos = await service.GetAllAsync();

            // Assert
            var entities = this.repository.All(false, false).ToList();
            var dates = entities.Select(e => e.ArrivalDate);

            Assert.That(dtos, Has.Exactly(entities.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(rt => !dates.Contains(rt.ArrivalDate)), Is.False, "Entities are not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationsService.GetAllArrivingFromDateAsync(DateOnly)"/>
        /// returns all arriving reservations on a given date.
        /// </summary>
        /// <param name="year">The year of the date.</param>
        /// <param name="month">The month of the date.</param>
        /// <param name="day">The day of the date.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllArrivingFromDateAsync(DateOnly date)
        [Test]
        [TestCase(2022, 1, 1)]
        public async Task GetAllArrivingFromDateAsyncShouldReturnCorrectReservations(int year, int month, int day)
        {
            // Arrange
            var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);
            var date = new DateOnly(year, month, day);

            // Act
            var dtos = await service.GetAllArrivingFromDateAsync(date);

            // Assert
            var entities = this.repository
                .All(false, false)
                .Where(r => r.Status == ReservationStatus.Arriving && r.ReservationDays
                    .Any(rd => rd.Date == date))
                .ToList();
            var dates = entities.Select(e => e.ArrivalDate);

            Assert.That(dtos, Has.Exactly(entities.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(rt => !dates.Contains(rt.ArrivalDate)), Is.False, "Entities are not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationsService.GetAllDepartingFromDateAsync(DateOnly)"/>
        /// returns all departing reservations on a given date.
        /// </summary>
        /// <param name="year">The year of the date.</param>
        /// <param name="month">The month of the date.</param>
        /// <param name="day">The day of the date.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllDepartingFromDateAsync(DateOnly date)
        [Test]
        [TestCase(2022, 3, 1)]
        public async Task GetAllDepartingFromDateAsyncShouldReturnCorrectReservations(int year, int month, int day)
        {
            // Arrange
            var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);
            var date = new DateOnly(year, month, day);

            // Act
            var dtos = await service.GetAllDepartingFromDateAsync(date);

            // Assert
            var entities = this.repository
                .All(false, false)
                .Where(r => r.Status == ReservationStatus.Departuring && r.ReservationDays
                    .Any(rd => rd.Date == date))
                .ToList();
            var dates = entities.Select(e => e.ArrivalDate);

            Assert.That(dtos, Has.Exactly(entities.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(rt => !dates.Contains(rt.ArrivalDate)), Is.False, "Entities are not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationsService.GetAllInHouseFromDateAsync(DateOnly)"/>
        /// returns all in house reservations on a given date.
        /// </summary>
        /// <param name="year">The year of the date.</param>
        /// <param name="month">The month of the date.</param>
        /// <param name="day">The day of the date.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllInHouseFromDateAsync(DateOnly date)
        [Test]
        [TestCase(2022, 2, 1)]
        public async Task GetAllInHouseFromDateAsyncShouldReturnCorrectReservations(int year, int month, int day)
        {
            // Arrange
            var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);
            var date = new DateOnly(year, month, day);

            // Act
            var dtos = await service.GetAllInHouseFromDateAsync(date);

            // Assert
            var entities = this.repository
                .All(false, false)
                .Where(r => r.Status == ReservationStatus.InHouse && r.ReservationDays
                    .Any(rd => rd.Date == date))
                .ToList();
            var dates = entities.Select(e => e.ArrivalDate);

            Assert.That(dtos, Has.Exactly(entities.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(rt => !dates.Contains(rt.ArrivalDate)), Is.False, "Entities are not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationsService.CreateReservationAsync(ReservationDto, int)"/>
        /// throws an exception when dto state is not valid.
        /// </summary>
        /// <param name="holderId">The id of a non existing guest profile.</param>
        /// <param name="roomTypeId">The id of a non existing room type.</param>
        /// <param name="roomId">The id of a free existing room.</param>
        // CreateReservationAsync(ReservationDto reservationDto, int roomId)
        [Test]
        [TestCase("1ca44253-44bf-4c4c-ada6-be955f365353", 1, 1)]
        public void CreateReservationAsyncShouldThrowWhenDtoIsInvalid(string holderId, int roomTypeId, int roomId)
        {
            // Arrange
            var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);
            var reservationGuid = Guid.NewGuid();
            var holderGuid = Guid.Parse(holderId);

            // Act
            var reservationDto = new ReservationDto()
            {
                Id = reservationGuid,
                ReservationHolderId = holderGuid,
                ArrivalDate = DateOnly.FromDateTime(DateTime.Today).AddDays(14),
                DepartureDate = DateOnly.FromDateTime(DateTime.Today).AddDays(13),
                RoomTypeId = roomTypeId,
                Adults = 0,
                Teenagers = 0,
                Children = 0,
            };

            var code = async () => await service.CreateReservationAsync(reservationDto, roomId);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await code(), "Method should throw exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationsService.CreateReservationAsync(ReservationDto, int)"/>
        /// throws an exception when arrival date is before today.
        /// </summary>
        /// <param name="holderId">The id of a existing guest profile.</param>
        /// <param name="roomTypeId">The id of a existing room type.</param>
        /// <param name="roomId">The id of a free existing room.</param>
        // CreateReservationAsync(ReservationDto reservationDto, int roomId)
        [Test]
        [TestCase("1ca44253-44bf-4c4c-ada6-be955f365353", 1, 1)]
        public void CreateReservationAsyncShouldThrowWhenArrivalDateIsBeforeToday(string holderId, int roomTypeId, int roomId)
        {
            // Arrange
            var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);
            var reservationGuid = Guid.NewGuid();
            var holderGuid = Guid.Parse(holderId);

            // Act
            var reservationDto = new ReservationDto()
            {
                Id = reservationGuid,
                ReservationHolderId = holderGuid,
                ArrivalDate = DateOnly.FromDateTime(DateTime.Today).AddDays(-1),
                DepartureDate = DateOnly.FromDateTime(DateTime.Today),
                RoomTypeId = roomTypeId,
                Adults = 1,
                Teenagers = 1,
                Children = 1,
            };

            var code = async () => await service.CreateReservationAsync(reservationDto, roomId);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationsService.CreateReservationAsync(ReservationDto, int)"/>
        /// creates a reservation and adds it to the database.
        /// </summary>
        /// <param name="holderId">The id of a existing guest profile.</param>
        /// <param name="roomTypeId">The id of a existing room type.</param>
        /// <param name="roomId">The id of a free existing room.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // CreateReservationAsync(ReservationDto reservationDto, int roomId)
        [Test]
        [TestCase("1ca44253-44bf-4c4c-ada6-be955f365353", 1, 1)]
        public async Task CreateReservationAsyncShouldCreateReservation(string holderId, int roomTypeId, int roomId)
        {
            // Arrange
            var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);
            var reservationGuid = Guid.NewGuid();
            var holderGuid = Guid.Parse(holderId);

            // Act
            var reservationDto = new ReservationDto()
            {
                Id = reservationGuid,
                ReservationHolderId = holderGuid,
                ArrivalDate = DateOnly.FromDateTime(DateTime.Today),
                DepartureDate = DateOnly.FromDateTime(DateTime.Today).AddDays(1),
                RoomTypeId = roomTypeId,
                Adults = 1,
                Teenagers = 1,
                Children = 1,
            };

            await service.CreateReservationAsync(reservationDto, roomId);

            // Assert
            var reservation = this.repository.Find(reservationGuid);
            Assert.That(reservation, Is.Not.Null);
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationsService.ExistsAsync(Guid, Common.QueryOptions{Dtos.Reservation.ReservationDto}?)"/>
        /// returns <see langword="false"/> for a non existing reservation.
        /// </summary>
        /// <param name="reservationId">The id of a non existing reservation.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // ExistsAsync(Guid id, QueryOptions<ReservationDto>? queryOptions = null)
        [Test]
        [TestCase("36dd7192-392c-4e99-8a8a-bcb4423009f5")]
        public async Task ExistsAsyncShouldReturFalseForNonExistingReservation(string reservationId)
        {
            // Arrange
            var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);
            var guid = Guid.Parse(reservationId);

            // Act
            bool result = await service.ExistsAsync(guid);

            // Assert
            Assert.That(result, Is.False, "Result should be false.");
        }

        /// <summary>
        /// This test checks whether <see cref="ReservationsService.ExistsAsync(Guid, Common.QueryOptions{Dtos.Reservation.ReservationDto}?)"/>
        /// returns <see langword="true"/> for a existing reservation.
        /// </summary>
        /// <param name="reservationId">The id of an existing reservation.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // ExistsAsync(Guid id, QueryOptions<ReservationDto>? queryOptions = null)
        [Test]
        [TestCase("033334e7-8fa3-4407-bdde-8fa6f3b8d175")]
        public async Task ExistsAsyncShouldReturTrueForExistingReservation(string reservationId)
        {
            // Arrange
            var service = new ReservationsService(this.repository, this.roomsService, this.reservationDaysService, this.mapper);
            var guid = Guid.Parse(reservationId);

            // Act
            bool result = await service.ExistsAsync(guid);

            // Assert
            Assert.That(result, Is.True, "Result should be true.");
        }
    }
}
