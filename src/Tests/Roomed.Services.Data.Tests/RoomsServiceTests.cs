// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomsServiceTests.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Tests
{
    using AutoMapper;
    using NUnit.Framework;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.RoomType;
    using Roomed.Tests.Common;

    /// <summary>
    /// This class contains all unit tests for <see cref="RoomsService"/>.
    /// </summary>
    [TestFixture]
    public class RoomsServiceTests
    {
        private readonly ICollection<Room> rooms = new List<Room>()
        {
            new Room()
            {
                Id = 1,
                Number = "10",
                Type = new RoomType() { Id = 1, Name = "Double Room" },
            },
            new Room()
            {
                Id = 2,
                Number = "11",
                Type = new RoomType() { Id = 1, Name = "Double Room" },
            },
            new Room()
            {
                Id = 3,
                Number = "12",
                Type = new RoomType() { Id = 2, Name = "Apartment" },
            },
        }.AsReadOnly();

        private IMapper mapper;
        private IDeletableEntityRepository<Room, int> repository;
        private IReservationDaysService reservationDaysService;

        /// <summary>
        /// This method is called before every test.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        [SetUp]
        public async Task SetUp()
        {
            this.mapper = MapperMock.Instance;
            this.repository = DeletableEntityRepositoryMock<Room, int>.Instance;
            this.reservationDaysService = ReservationDaysServiceMock.Instance;

            foreach (var item in this.rooms)
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
        /// This test checks whether <see cref="RoomsService.GetAllAsync(Dtos.RoomType.RoomTypeDto?, Common.QueryOptions{Dtos.Room.RoomDto}?)"/>
        /// returns all not deleted rooms.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllAsync(RoomTypeDto? roomType = null, QueryOptions<RoomDto>? queryOptions = null)
        [Test]
        public async Task GetAllAsyncShouldReturnAllNotDeletedRooms()
        {
            // Arrange
            var service = new RoomsService(this.repository, this.reservationDaysService, this.mapper);

            // Act
            var dtos = await service.GetAllAsync();

            // Assert
            var entities = this.repository.All(false, false).ToList();
            var numbers = entities.Select(e => e.Number);

            Assert.That(dtos, Has.Exactly(entities.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(r => !numbers.Contains(r.Number)), Is.False, "Entities are not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="RoomsService.GetAllAsync(RoomTypeDto?, Common.QueryOptions{Dtos.Room.RoomDto}?)"/>
        /// returns all not deleted rooms of type.
        /// </summary>
        /// <param name="roomType">The name of the room type.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllAsync(RoomTypeDto? roomType = null, QueryOptions<RoomDto>? queryOptions = null)
        [Test]
        [TestCase("Double Room")]
        public async Task GetAllAsyncShouldReturnAllNotDeletedRoomsOfType(string roomType)
        {
            // Arrange
            var service = new RoomsService(this.repository, this.reservationDaysService, this.mapper);

            // Act
            var roomTypeDto = new RoomTypeDto()
            {
                Name = roomType,
            };

            var dtos = await service.GetAllAsync(roomTypeDto);

            // Assert
            var entities = this.repository
                .All(false, false)
                .Where(r => r.Type.Name == roomType)
                .ToList();
            var numbers = entities.Select(e => e.Number);

            Assert.That(dtos, Has.Exactly(entities.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(r => !numbers.Contains(r.Number)), Is.False, "Entities are not correct.");
            Assert.That(dtos.All(r => r.Type.Name == roomType), Is.True, "Entity's room type is not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="RoomsService.ExistsAsync(int, Common.QueryOptions{Dtos.Room.RoomDto}?)"/>
        /// returns <see langword="true"/> for an existing room.
        /// </summary>
        /// <param name="roomId">The id of an existing room.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // ExistsAsync(int id, QueryOptions<RoomDto>? queryOptions = null)
        [Test]
        [TestCase(1)]
        public async Task ExistAsyncShouldReturnTrueForExistingRoom(int roomId)
        {
            // Arrange
            var service = new RoomsService(this.repository, this.reservationDaysService, this.mapper);

            // Act
            var result = await service.ExistsAsync(roomId);

            // Assert
            Assert.IsTrue(result, "Result should be true.");
        }

        /// <summary>
        /// This test checks whether <see cref="RoomsService.ExistsAsync(int, Common.QueryOptions{Dtos.Room.RoomDto}?)"/>
        /// returns <see langword="false"/> for a non existing room.
        /// </summary>
        /// <param name="roomId">The id of a non existing room.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // ExistsAsync(int id, QueryOptions<RoomDto>? queryOptions = null)
        [Test]
        [TestCase(0)]
        public async Task ExistAsyncShouldReturnFalseForNonExistingRoom(int roomId)
        {
            // Arrange
            var service = new RoomsService(this.repository, this.reservationDaysService, this.mapper);

            // Act
            var result = await service.ExistsAsync(roomId);

            // Assert
            Assert.IsFalse(result, "Result should be false.");
        }

        /// <summary>
        /// This test checks whether <see cref="RoomsService.GetAllFreeRoomsAsync(DateOnly, RoomTypeDto?, Common.QueryOptions{Dtos.Room.RoomDto}?)"/>
        /// should return all free rooms of type for a date.
        /// </summary>
        /// <param name="roomType">The name of the room type.</param>
        /// <param name="year">The year of the date.</param>
        /// <param name="month">The month of the date.</param>
        /// <param name="day">The day of the date.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllFreeRoomsAsync(DateOnly date, RoomTypeDto? roomType = null, QueryOptions<RoomDto>? queryOptions = null)
        [Test]
        [TestCase("Double Room", 2022, 6, 13)]
        public async Task GetAllFreeRoomsAsyncShouldReturnAllFreeRoomsOfTypeForDate(string roomType, int year, int month, int day)
        {
            // Arrange
            var service = new RoomsService(this.repository, this.reservationDaysService, this.mapper);
            var date = new DateOnly(year, month, day);

            // Act
            var roomTypeDto = new RoomTypeDto()
            {
                Name = roomType,
            };

            var dtos = await service.GetAllFreeRoomsAsync(date, roomTypeDto);

            // Assert
            var reservationDays = await this.reservationDaysService.GetAllForDateAsync(date);

            var freeRooms = this.repository
                .All(false, false)
                .Where(r => r.Type.Name == roomType)
                .Where(r => !reservationDays.Any(rd => rd.Room.Id == r.Id))
                .ToList();

            var numbers = freeRooms.Select(e => e.Number);

            Assert.That(dtos, Has.Exactly(freeRooms.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(r => !numbers.Contains(r.Number)), Is.False, "Entities are not correct.");
            Assert.That(dtos.All(r => r.Type.Name == roomType), Is.True, "Entity's room type is not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="RoomsService.GetAllFreeRoomsAsync(DateOnly, DateOnly, RoomTypeDto?, Common.QueryOptions{Dtos.Room.RoomDto}?)"/>
        /// returns all free rooms of type for a given period.
        /// </summary>
        /// <param name="roomType">The name of the room type.</param>
        /// <param name="startYear">The year of the start of the period.</param>
        /// <param name="startMonth">The month of the start of the period.</param>
        /// <param name="startDay">The day of the start of the period.</param>
        /// <param name="endYear">The year of the end of the period.</param>
        /// <param name="endMonth">The month of the end of the period.</param>
        /// <param name="endDay">The day of the end of the period.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllFreeRoomsAsync(DateOnly startDate, DateOnly endDate, RoomTypeDto? roomType = null, QueryOptions<RoomDto>? queryOptions = null)
        [Test]
        [TestCase("Double Room", 2022, 6, 10, 2022, 6, 15)]
        public async Task GetAllFreeRoomsAsyncShouldReturnAllFreeRoomsOfTypeForAPeriod(
            string roomType,
            int startYear, int startMonth, int startDay,
            int endYear, int endMonth, int endDay)
        {
            // Arrange
            var service = new RoomsService(this.repository, this.reservationDaysService, this.mapper);
            var startDate = new DateOnly(startYear, startMonth, startDay);
            var endDate = new DateOnly(endYear, endMonth, endDay);

            // Act
            var roomTypeDto = new RoomTypeDto()
            {
                Name = roomType,
            };

            var dtos = await service.GetAllFreeRoomsAsync(startDate, endDate, roomTypeDto);

            // Assert
            var reservationDays = await this.reservationDaysService.GetAllForPeriodAsync(startDate, endDate);

            var freeRooms = this.repository
                .All(false, false)
                .Where(r => r.Type.Name == roomType)
                .Where(r => !reservationDays.Any(rd => rd.Room.Id == r.Id))
                .ToList();

            var numbers = freeRooms.Select(e => e.Number);

            Assert.That(dtos, Has.Exactly(freeRooms.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(r => !numbers.Contains(r.Number)), Is.False, "Entities are not correct.");
            Assert.That(dtos.All(r => r.Type.Name == roomType), Is.True, "Entity's room type is not correct.");
        }
    }
}
