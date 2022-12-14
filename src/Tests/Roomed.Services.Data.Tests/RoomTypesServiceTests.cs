// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomTypesServiceTests.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Tests
{
    using System.Linq;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using NUnit.Framework;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Tests.Common;

    /// <summary>
    /// This class contains all unit tests for <see cref="RoomTypesService"/>.
    /// </summary>
    [TestFixture]
    public class RoomTypesServiceTests
    {
        private readonly ICollection<RoomType> roomTypes = new List<RoomType>()
        {
            new RoomType()
            {
                Id = 1,
                Name = "Double room",
                IsDeleted = false,
            },
            new RoomType()
            {
                Id = 2,
                Name = "Apartment suite",
                IsDeleted = false,
            },
            new RoomType()
            {
                Id = 3,
                Name = "Studio",
                IsDeleted = true,
            },
        };

        private IMapper mapper;
        private IDeletableEntityRepository<RoomType, int> repository;

        /// <summary>
        /// This method is called before every test.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        [SetUp]
        public async Task SetUp()
        {
            this.mapper = MapperMock.Instance;
            this.repository = DeletableEntityRepositoryMock<RoomType, int>.Instance;

            foreach (var item in this.roomTypes)
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
        /// This test check whether <see cref="RoomTypesService.GetAllAsync(Common.QueryOptions{Dtos.RoomType.RoomTypeDto}?)"/>
        /// returns all not deleted <see cref="RoomType"/> entities.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllAsync(QueryOptions<RoomTypeDto>? queryOptions = null)
        [Test]
        public async Task GetAllAsyncShouldReturnAllNotDeletedRoomTypes()
        {
            // Arrange
            RoomTypesService service = new RoomTypesService(this.repository, this.mapper);

            // Act
            var dtos = await service.GetAllAsync();

            // Assert
            var entities = this.repository.All(false, false).ToList();
            var names = entities.Select(e => e.Name);

            Assert.That(dtos, Has.Exactly(entities.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(rt => !names.Contains(rt.Name)), Is.False, "Entities are not correct.");
        }

        /// <summary>
        /// This test check whether <see cref="RoomTypesService.GetAsync(int, Common.QueryOptions{Dtos.RoomType.RoomTypeDto}?)"/>
        /// returns the correct room type by a given id.
        /// </summary>
        /// <param name="id">The id of the room type.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAsync(int id, QueryOptions<RoomTypeDto>? queryOptions = null)
        [Test]
        [TestCase(3)]
        public async Task GetAsyncShouldReturnExactRoomType(int id)
        {
            // Arrange
            RoomTypesService service = new RoomTypesService(this.repository, this.mapper);

            // Act
            var dto = await service.GetAsync(id);

            // Assert
            var entity = this.repository.Find(id);

            Assert.That(dto.Id, Is.EqualTo(entity.Id), "Entity's id is not correct.");
            Assert.That(dto.Name, Is.EqualTo(entity.Name), "Entity's name is not correct.");
        }

        /// <summary>
        /// This test check whether <see cref="RoomTypesService.GetAsync(int, Common.QueryOptions{Dtos.RoomType.RoomTypeDto}?)"/>
        /// throws an exception when no room type with a specified id exists.
        /// </summary>
        /// <param name="id">The id of a non existing room type.</param>
        // GetAsync(int id, QueryOptions<RoomTypeDto>? queryOptions = null)
        [Test]
        [TestCase(6)]
        public void GetAsyncShouldThrowWhenEntityDoesNotExist(int id)
        {
            // Arrange
            RoomTypesService service = new RoomTypesService(this.repository, this.mapper);

            // Act
            var code = async () => await service.GetAsync(id);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="RoomTypesService.ExistsAsync(int, Common.QueryOptions{Dtos.RoomType.RoomTypeDto}?)"/>
        /// returns true for an existing room type.
        /// </summary>
        /// <param name="id">The id of an existing room type.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // ExistsAsync(int id, QueryOptions<RoomTypeDto>? queryOptions = null)
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task ExistsAsyncShouldReturnTrueForExistingRoomTypes(int id)
        {
            // Arrange
            RoomTypesService service = new RoomTypesService(this.repository, this.mapper);

            // Act
            var result = await service.ExistsAsync(id);

            // Assert
            Assert.That(result, Is.True, "Result should be true.");
        }

        /// <summary>
        /// This test checks whether <see cref="RoomTypesService.ExistsAsync(int, Common.QueryOptions{Dtos.RoomType.RoomTypeDto}?)"/>
        /// returns false for non existing room types.
        /// </summary>
        /// <param name="id">The id of a non existing room type.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // ExistsAsync(int id, QueryOptions<RoomTypeDto>? queryOptions = null)
        [Test]
        [TestCase(4)]
        [TestCase(0)]
        public async Task ExistsAsyncShouldReturnFalseForNonExistingRoomTypes(int id)
        {
            // Arrange
            RoomTypesService service = new RoomTypesService(this.repository, this.mapper);

            // Act
            var result = await service.ExistsAsync(id);

            // Assert
            Assert.That(result, Is.False, "Result should be false.");
        }
    }
}
