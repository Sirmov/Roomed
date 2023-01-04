// |-----------------------------------------------------------------------------------------------------|
// <copyright file="BaseServiceTests.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Tests
{
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.Internal;
    using NUnit.Framework;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Dtos.ReservationNote;
    using Roomed.Services.Data.Tests.TestClasses;
    using Roomed.Tests.Common;

    using static Roomed.Common.Constants.DataConstants.ReservationNote;

    /// <summary>
    /// This class contains all unit tests for <see cref="BaseService{TEntity, TKey}"/>.
    /// </summary>
    [TestFixture]
    public class BaseServiceTests
    {
        private readonly ICollection<ReservationNote> reservationNotes = new List<ReservationNote>()
        {
            new ReservationNote()
            {
                Id = Guid.Parse("2bfff802-5afb-4bbb-96b3-27c98161ff00"),
                Body = "Reservation note #1",
                IsDeleted = false,
            },
            new ReservationNote()
            {
                Id = Guid.Parse("bb2f7b6c-d8d9-4e2c-b14b-3bd98e18ad86"),
                Body = "Reservation note #2",
                IsDeleted = true,
            },
            new ReservationNote()
            {
                Id = Guid.Parse("08bd1b0d-15fd-4d2e-9f59-979d09da1133"),
                Body = "Reservation note #3",
                IsDeleted = false,
            },
        }.AsReadOnly();

        private IMapper mapper;
        private IDeletableEntityRepository<ReservationNote, Guid> repository;

        /// <summary>
        /// This method is called before every test.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        [SetUp]
        public async Task SetUp()
        {
            this.mapper = MapperMock.Instance;
            this.repository = DeletableEntityRepositoryMock<ReservationNote, Guid>.Instance;

            foreach (var item in this.reservationNotes)
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
        /// This test checks whether <see cref="BaseService{TEntity, TKey}.GetAllAsync{TDto}(QueryOptions{TDto}?)"/>
        /// returns all not deleted entities.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllAsync<TDto>(QueryOptions<TDto>? queryOptions = null)
        [Test]
        public async Task GetAllAsyncShouldReturnAllNotDeletedEntities()
        {
            // Arrange
            BaseService<ReservationNote, Guid> baseService = new (this.repository, this.mapper);

            // Act
            var dtos = await baseService.GetAllAsync<ReservationNoteDto>();

            // Assert
            Assert.That(dtos, Has.Count.EqualTo(2), "Entities count is not correct.");
            Assert.That(dtos, Has.All.Matches<object>(x => x is ReservationNoteDto), "Some entities are not of the correct type.");
        }

        /// <summary>
        /// This test check whether <see cref="BaseService{TEntity, TKey}.GetAllAsync{TDto}(QueryOptions{TDto}?)"/>
        /// returns all entities including deleted ones.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllAsync<TDto>(QueryOptions<TDto>? queryOptions = null)
        [Test]
        public async Task GetAllAsyncShouldReturnAllEntitiesWithDeleted()
        {
            // Arrange
            BaseService<ReservationNote, Guid> baseService = new (this.repository, this.mapper);

            // Act
            var dtos = await baseService.GetAllAsync<ReservationNoteDto>(new ()
            {
                WithDeleted = true,
            });

            // Assert
            Assert.That(dtos, Has.Count.EqualTo(3), "Entities count is not correct.");
            Assert.That(dtos, Has.All.Matches<object>(x => x is ReservationNoteDto), "Some entities are not of the correct type.");
        }

        /// <summary>
        /// This test checks whether <see cref="BaseService{TEntity, TKey}.ModifyQuery{TDto}(IQueryable{TDto}, QueryOptions{TDto})"/>
        /// ordering works correctly.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // ModifyQuery<TDto>(IQueryable<TDto> query, QueryOptions<TDto> queryOptions)
        [Test]
        public async Task GetAllAsyncShouldReturnAllEntitiesInCorrectOrder()
        {
            // Arrange
            BaseService<ReservationNote, Guid> baseService = new (this.repository, this.mapper);

            // Act
            var dtos = await baseService.GetAllAsync<ReservationNoteDto>(new ()
            {
                OrderOptions = new ()
                {
                    new OrderOption<ReservationNoteDto>(e => e.Body, OrderByOrder.Ascending),
                },
                WithDeleted = true,
            });

            // Assert
            var reservationNotes = await this.repository.All(true, true).ToListAsync();
            Assert.That(dtos, Has.Count.EqualTo(3), "Entities count is not correct.");
            Assert.That(dtos, Has.All.Matches<object>(x => x is ReservationNoteDto), "Some entities are not of the correct type.");
            Assert.That(dtos.First().Body, Is.EqualTo(reservationNotes.First().Body), "Entities are not in correct order.");
        }

        // [Test]
        // [TestCase(1)]
        // [TestCase(2)]
        // public async Task GetAllAsyncShouldReturnAllEntitiesSkippingFirstNAmount(int n)
        // {
        //    // Arrange
        //    BaseService<ReservationNote, Guid> baseService = new (this.repository, this.mapper);

        //    // Act
        //    var dtos = await baseService.GetAllAsync<ReservationNoteDto>(new ()
        //    {
        //        OrderOptions = new ()
        //        {
        //            new OrderOption<ReservationNoteDto>(rn => rn.Id, OrderByOrder.Ascending),
        //        },
        //        Skip = n,
        //        WithDeleted = true,
        //    });

        //    // Assert
        //    var reservationNotes = await this.repository.All(true, true).ToListAsync();
        //    Assert.That(dtos.Count, Is.EqualTo(reservationNotes.Count - n), "Entities count is not correct.");
        //    Assert.That(dtos, Has.All.Matches<object>(x => x is ReservationNoteDto), "Some entities are not of the correct type.");
        //    Assert.That(dtos.First().Body,
        //        Is.EqualTo(reservationNotes[reservationNotes.Count - n - 1].Body), "Entities are not in correct order.");
        // }

        // [Test]
        // [TestCase(1)]
        // [TestCase(2)]
        // public async Task GetAllAsyncShouldReturnAllEntitiesTakingFirstNAmount(int n)
        // {
        //    // Arrange
        //    BaseService<ReservationNote, Guid> baseService = new(this.repository, this.mapper);

        //    // Act
        //    var dtos = await baseService.GetAllAsync<ReservationNoteDto>(new()
        //    {
        //        Take = n,
        //        WithDeleted = true
        //    });

        //    // Assert
        //    var reservationNotes = await this.repository.All(true, true).ToListAsync();
        //    Assert.That(dtos.Count, Is.EqualTo(reservationNotes.Count - n), "Entities count is not correct.");
        //    Assert.That(dtos, Has.All.Matches<object>(x => x is ReservationNoteDto), "Some entities are not of the correct type.");
        //    Assert.That(dtos.Last().Body,
        //        Is.EqualTo(reservationNotes[n - 1].Body), "Entities are not in correct order.");
        // }

        /// <summary>
        /// This test checks whether <see cref="BaseService{TEntity, TKey}.GetAsync{TDto}(TKey, QueryOptions{TDto}?)"/>
        /// throws an exception when a entity with a specified id does not exist.
        /// </summary>
        /// <param name="id">The id of a non existing entity.</param>
        // GetAsync<TDto>(TKey id, QueryOptions<TDto>? queryOptions = null)
        [Test]
        [TestCase("8d162afd-bcb2-4ccb-81c0-96965f7177d0")]
        public void GetAsyncShouldThrowWhenEntityDoesNotExist(string id)
        {
            // Arrange
            BaseService<ReservationNote, Guid> baseService = new (this.repository, this.mapper);
            Guid guid = Guid.Parse(id);

            // Act
            Func<Task> code = async () => await baseService.GetAsync<ReservationNoteDto>(guid);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Should throw when entity is not found.");
        }

        /// <summary>
        /// This test checks whether <see cref="BaseService{TEntity, TKey}.GetAsync{TDto}(TKey, QueryOptions{TDto}?)"/>
        /// returns the correct entity by a given id.
        /// </summary>
        /// <param name="id">The id of an existing entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAsync<TDto>(TKey id, QueryOptions<TDto>? queryOptions = null)
        [Test]
        [TestCase("bb2f7b6c-d8d9-4e2c-b14b-3bd98e18ad86")]
        public async Task GetAsyncShouldReturnEntityDtoWithGivenId(string id)
        {
            // Arrange
            BaseService<ReservationNote, Guid> baseService = new (this.repository, this.mapper);
            Guid guid = Guid.Parse(id);

            // Act
            var dto = await baseService.GetAsync<ReservationNoteDto>(guid);

            // Assert
            var reservationNotes = await this.repository.All(true, true).ToListAsync();
            var reservationNote = reservationNotes.First(rn => rn.Id == guid);
            Assert.Multiple(() =>
            {
                Assert.That(reservationNote, Is.Not.Null, "Entity does not exist.");
                Assert.That(dto is ReservationNoteDto, Is.True, "Entity is not of correct type.");
                Assert.That(dto.Body, Is.EqualTo(reservationNote.Body), "Entities do not match.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="BaseService{TEntity, TKey}.ValidateDto{TDto}(TDto)"/>
        /// returns false for a non valid dto.
        /// </summary>
        // ValidateDto<TDto>(TDto dto)
        [Test]
        public void ValidateDtoShouldReturnFalse()
        {
            // Arrange
            BaseServiceTest<ReservationNote, Guid> baseService = new (this.repository, this.mapper);

            var shortDto = new ReservationNoteDto()
            {
                Body = new string('*', BodyMinLength - 1),
            };

            var longDto = new ReservationNoteDto()
            {
                Body = new string('*', BodyMaxLength + 1),
            };

            // Act
            bool shortResult = baseService.ValidateDto(shortDto);
            bool longResult = baseService.ValidateDto(longDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(shortResult, Is.False, "Validation result should be false.");
                Assert.That(longResult, Is.False, "Validation result should be false.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="BaseService{TEntity, TKey}.ValidateDto{TDto}(TDto)"/>
        /// returns true for a valid dto.
        /// </summary>
        // ValidateDto<TDto>(TDto dto)
        [Test]
        public void ValidateDtoShouldReturnTrue()
        {
            // Arrange
            BaseServiceTest<ReservationNote, Guid> baseService = new (this.repository, this.mapper);

            var shortDto = new ReservationNoteDto()
            {
                Body = new string('*', BodyMinLength),
            };

            var longDto = new ReservationNoteDto()
            {
                Body = new string('*', BodyMaxLength),
            };

            // Act
            bool shortResult = baseService.ValidateDto(shortDto);
            bool longResult = baseService.ValidateDto(longDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(shortResult, Is.True, "Validation result should be true.");
                Assert.That(longResult, Is.True, "Validation result should be true.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="BaseService{TEntity, TKey}.ValidateDto{TDto}(TDto)"/>
        /// throw an exception when the dto is null.
        /// </summary>
        // ValidateDto<TDto>(TDto dto)
        [Test]
        public void ValidateDtoShouldThrowWhenDtoIsNull()
        {
            // Arrange
            BaseServiceTest<ReservationNote, Guid> baseService = new (this.repository, this.mapper);
            ReservationNoteDto? dto = null;

            // Act
            var code = () => baseService.ValidateDto(dto);

            // Assert
            Assert.Throws<ArgumentNullException>(() => code(), "Method should throw when dto is null.");
        }
    }
}
