// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfilesServiceTests.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Tests
{
    using AutoMapper;
    using NUnit.Framework;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Tests.Common;

    using static Roomed.Common.DataConstants.Profile;

    using Profile = Roomed.Data.Models.Profile;

    /// <summary>
    /// This class contains all unit tests for <see cref="ProfilesService"/>.
    /// </summary>
    [TestFixture]
    public class ProfilesServiceTests
    {
        private readonly ICollection<Profile> profiles = new List<Profile>()
        {
            new Profile()
            {
                Id = Guid.Parse("63af5322-14ce-4abf-933f-ee16dc3f952d"),
                FirstName = "John",
                LastName = "Smith",
                IsDeleted = false,
            },
            new Profile()
            {
                Id = Guid.Parse("01e26ba7-52df-4293-a4cb-2bc20cd2e733"),
                FirstName = "Oliver",
                LastName = "Kemp",
                IsDeleted = false,
            },
            new Profile()
            {
                Id = Guid.Parse("862ed05a-18c3-43f7-addd-0ef3cb40826e"),
                FirstName = "Eric",
                LastName = "Walton",
                IsDeleted = true,
            },
        };

        private IMapper mapper;
        private IDeletableEntityRepository<Profile, Guid> repository;

        /// <summary>
        /// This method is called before every test.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        [SetUp]
        public async Task SetUp()
        {
            this.mapper = MapperMock.Instance;
            this.repository = DeletableEntityRepositoryMock<Profile, Guid>.Instance;

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
        /// This test check whether <see cref="ProfilesService.GetAllAsync(Common.QueryOptions{Dtos.Profile.DetailedProfileDto}?)"/>
        /// returns all not deleted <see cref="Profile"/> entities.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllAsync(QueryOptions<DetailedProfileDto>? queryOptions = null)
        [Test]
        public async Task GetAllAsyncShouldReturnAllNotDeletedProfiles()
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);

            // Act
            var dtos = await service.GetAllAsync();

            // Assert
            var entities = this.repository.All(false, false).ToList();
            var names = entities.Select(e => e.LastName);

            Assert.That(dtos, Has.Exactly(entities.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(rt => !names.Contains(rt.LastName)), Is.False, "Entities are not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="ProfilesService.GetAsync(Guid, Common.QueryOptions{Dtos.Profile.DetailedProfileDto}?)"/>
        /// returns the correct profile by a given id.
        /// </summary>
        /// <param name="id">The id of the profile.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAsync(Guid id, QueryOptions<DetailedProfileDto>? queryOptions = null)
        [Test]
        [TestCase("01e26ba7-52df-4293-a4cb-2bc20cd2e733")]
        public async Task GetAsyncShouldReturnExactProfile(string id)
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);
            Guid guid = Guid.Parse(id);

            // Act
            var dto = await service.GetAsync(guid);

            // Assert
            var entity = this.repository.Find(guid);

            Assert.That(dto.Id, Is.EqualTo(entity.Id), "Entity's id is not correct.");
            Assert.That(dto.LastName, Is.EqualTo(entity.LastName), "Entity's name is not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="ProfilesService.GetAsync(Guid, Common.QueryOptions{Dtos.Profile.DetailedProfileDto}?)"/>
        /// throws an exception when no profile with a specified id exists.
        /// </summary>
        /// <param name="id">The id of a non existing profile.</param>
        // GetAsync(Guid id, QueryOptions<DetailedProfileDto>? queryOptions = null)
        [Test]
        [TestCase("212bb3ec-b975-4798-9a92-64b147efba30")]
        public void GetAsyncShouldThrowWhenProfileDoesNotExist(string id)
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);
            Guid guid = Guid.Parse(id);

            // Act
            var code = async () => await service.GetAsync(guid);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="RoomTypesService.ExistsAsync(int, Common.QueryOptions{Dtos.RoomType.RoomTypeDto}?)"/>
        /// returns <see langword="true"/> for an existing profile.
        /// </summary>
        /// <param name="id">The id of an existing profile.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // ExistsAsync(Guid id, QueryOptions<DetailedProfileDto>? queryOptions = null)
        [Test]
        [TestCase("63af5322-14ce-4abf-933f-ee16dc3f952d")]
        [TestCase("01e26ba7-52df-4293-a4cb-2bc20cd2e733")]
        [TestCase("862ed05a-18c3-43f7-addd-0ef3cb40826e")]
        public async Task ExistsAsyncShouldReturnTrueForExistingProfile(string id)
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);
            Guid guid = Guid.Parse(id);

            // Act
            var result = await service.ExistsAsync(guid);

            // Assert
            Assert.That(result, Is.True, "Result should be true.");
        }

        /// <summary>
        /// This test checks whether <see cref="RoomTypesService.ExistsAsync(int, Common.QueryOptions{Dtos.RoomType.RoomTypeDto}?)"/>
        /// returns <see langword="false"/> for a non existing profile.
        /// </summary>
        /// <param name="id">The id of a non existing profile.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // ExistsAsync(Guid id, QueryOptions<DetailedProfileDto>? queryOptions = null)
        [Test]
        [TestCase("369edf75-344b-444c-a088-47b587ce1939")]
        public async Task ExistsAsyncShouldReturnFalseForNonExistingProfile(string id)
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);
            Guid guid = Guid.Parse(id);

            // Act
            var result = await service.ExistsAsync(guid);

            // Assert
            Assert.That(result, Is.False, "Result should be false.");
        }

        /// <summary>
        /// This test checks whether <see cref="ProfilesService.DeleteAsync(Guid)"/>
        /// throws an exception when no profile with specified id can be found.
        /// </summary>
        /// <param name="id">The id of a non existing profile.</param>
        // DeleteAsync(Guid id)
        [Test]
        [TestCase("b5203cb5-8b78-48a6-878b-03b251708991")]
        public void DeleteAsyncShouldThrowWhenProfileDoesNotExist(string id)
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);
            Guid guid = Guid.Parse(id);

            // Act
            var code = async () => await service.DeleteAsync(guid);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="ProfilesService.DeleteAsync(Guid)"/>
        /// deletes an existing guest profile by setting its isDeleted flag to true.
        /// </summary>
        /// <param name="id">The id of an existing entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // DeleteAsync(Guid id)
        [Test]
        [TestCase("01e26ba7-52df-4293-a4cb-2bc20cd2e733")]
        public async Task DeleteAsyncShouldMarkProfileAsDeleted(string id)
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);
            Guid guid = Guid.Parse(id);

            // Act
            await service.DeleteAsync(guid);

            // Assert
            var entity = this.repository.Find(guid);
            Assert.That(entity.IsDeleted, Is.True, "Entity IsDeleted flag is not set to true.");
        }

        /// <summary>
        /// This test checks whether <see cref="ProfilesService.EditAsync(Guid, Dtos.Profile.DetailedProfileDto)"/>
        /// throws an error when no profile with a specified id can be found.
        /// </summary>
        /// <param name="id">The id of an non existing profile.</param>
        // EditAsync(Guid id, DetailedProfileDto newProfile)
        [Test]
        [TestCase("8445a2a4-4856-472f-a494-3d2024179558")]
        public void EditAsyncShouldThrowWhenProfileDoesNotExist(string id)
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);
            Guid guid = Guid.Parse(id);

            // Act
            var code = async () => await service.EditAsync(guid, null!);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="ProfilesService.DeleteAsync(Guid)"/>
        /// throws an exception when the dto state is not valid.
        /// </summary>
        /// <param name="id">The id of an existing profile.</param>
        // EditAsync(Guid id, DetailedProfileDto newProfile)
        [Test]
        [TestCase("63af5322-14ce-4abf-933f-ee16dc3f952d")]
        public void EditAsyncShouldThrowWhenDtoIsNotValid(string id)
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);
            Guid guid = Guid.Parse(id);

            // Act
            var emptyDto = new DetailedProfileDto();
            var maxDto = new DetailedProfileDto()
            {
                FirstName = new string('*', FirstNameMaxLength + 1),
                LastName = new string('*', LastNameMaxLength + 1),
            };
            var minDto = new DetailedProfileDto()
            {
                FirstName = new string('*', FirstNameMinLength - 1),
                LastName = new string('*', LastNameMinLength - 1),
            };

            var code = async (DetailedProfileDto dto) => await service.EditAsync(guid, dto);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await code(emptyDto), "Method should throw an exception.");
            Assert.ThrowsAsync<ArgumentException>(async () => await code(maxDto), "Method should throw an exception.");
            Assert.ThrowsAsync<ArgumentException>(async () => await code(minDto), "Method should throw an exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="ProfilesService.EditAsync(Guid, DetailedProfileDto)"/>
        /// modifies the entity's data.
        /// </summary>
        /// <param name="id">The id of the entity to be edited.</param>
        /// <param name="firstName">The new first name.</param>
        /// <param name="lastName">The new last name.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // EditAsync(Guid id, DetailedProfileDto newProfile)
        [Test]
        [TestCase("63af5322-14ce-4abf-933f-ee16dc3f952d", "Evgeni", "Petrov")]
        public async Task EditAsyncShouldModifyProfileData(string id, string firstName, string lastName)
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);
            Guid guid = Guid.Parse(id);

            // Act
            var dto = new DetailedProfileDto()
            {
                Id = guid,
                FirstName = firstName,
                LastName = lastName,
                Birthdate = DateOnly.FromDateTime(DateTime.Today).AddDays(-300),
                Gender = Gender.Male,
                Nationality = "Bulgarian",
                NationalityCode = "BG",
            };

            await service.EditAsync(guid, dto);

            // Assert
            var entity = this.repository.Find(guid);

            Assert.That(entity.FirstName, Is.EqualTo(firstName), "Entity's first name is not modified.");
            Assert.That(entity.LastName, Is.EqualTo(lastName), "Entity's last name is not modified.");
            Assert.That(entity.Gender, Is.EqualTo(Gender.Male), "Entity's gender is not modified.");
            Assert.That(entity.Nationality, Is.EqualTo("Bulgarian"), "Entity's nationality is not modified.");
            Assert.That(entity.NationalityCode, Is.EqualTo("BG"), "Entity's nationality code is not modified.");
        }

        /// <summary>
        /// This test checks whether <see cref="ProfilesService.CreateDetailedAsync(DetailedProfileDto)"/>
        /// creates a new profile entity and adds it to the database.
        /// </summary>
        /// <param name="firstName">The first name of the new profile.</param>
        /// <param name="lastName">The last name of the new profile.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // CreateDetailedAsync(DetailedProfileDto profileDto)
        [Test]
        [TestCase("Bella", "Mclean")]
        public async Task CreateDetailedAsyncShouldCreateAndAddANewProfile(string firstName, string lastName)
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);
            var guid = Guid.NewGuid();

            // Act
            var dto = new DetailedProfileDto()
            {
                Id = guid,
                FirstName = firstName,
                LastName = lastName,
                Birthdate = DateOnly.FromDateTime(DateTime.Today).AddDays(-300),
                Gender = Gender.Female,
                Nationality = "British",
                NationalityCode = "GB",
            };

            await service.CreateDetailedAsync(dto);

            // Assert
            var entity = this.repository.Find(guid);

            Assert.That(entity.FirstName, Is.EqualTo(firstName), "Entity's first name is not correct.");
            Assert.That(entity.LastName, Is.EqualTo(lastName), "Entity's last name is not correct.");
            Assert.That(entity.Gender, Is.EqualTo(Gender.Female), "Entity's gender is not correct.");
            Assert.That(entity.Nationality, Is.EqualTo("British"), "Entity's nationality is not correct.");
            Assert.That(entity.NationalityCode, Is.EqualTo("GB"), "Entity's nationality code is not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="ProfilesService.CreateDetailedAsync(DetailedProfileDto)"/>
        /// throws an exception when dto state is not valid.
        /// </summary>
        // CreateDetailedAsync(DetailedProfileDto profileDto)
        [Test]
        public void CreateDetailedAsyncShouldThrowWhenDtoIsNotValid()
        {
            // Arrange
            ProfilesService service = new ProfilesService(this.repository, this.mapper);

            // Act
            var emptyDto = new DetailedProfileDto();
            var maxDto = new DetailedProfileDto()
            {
                FirstName = new string('*', FirstNameMaxLength + 1),
                LastName = new string('*', LastNameMaxLength + 1),
            };
            var minDto = new DetailedProfileDto()
            {
                FirstName = new string('*', FirstNameMinLength - 1),
                LastName = new string('*', LastNameMinLength - 1),
            };

            var code = async (DetailedProfileDto dto) => await service.CreateDetailedAsync(dto);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await code(emptyDto), "Method should throw an exception.");
            Assert.ThrowsAsync<ArgumentException>(async () => await code(maxDto), "Method should throw an exception.");
            Assert.ThrowsAsync<ArgumentException>(async () => await code(minDto), "Method should throw an exception.");
        }
    }
}
