// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IdentityDocumentsServiceTests.cs" company="Roomed">
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
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.IdentityDocument;
    using Roomed.Tests.Common;

    using static Roomed.Common.DataConstants.IdentityDocument;

    /// <summary>
    /// This class contains all unit tests for <see cref="IdentityDocumentsService"/>.
    /// </summary>
    [TestFixture]
    public class IdentityDocumentsServiceTests
    {
        private readonly ICollection<IdentityDocument> identityDocuments = new List<IdentityDocument>()
        {
            new IdentityDocument()
            {
                Id = Guid.Parse("a2c96289-6848-4f1c-92eb-31e88cf6b539"),
                NameInDocument = "Justin Reed Rowland",
                IsDeleted = false,
            },
            new IdentityDocument()
            {
                Id = Guid.Parse("d967bbec-6b55-46d7-a33f-ef896cfc3a8f"),
                NameInDocument = "Carrie Kennedy Fleming",
                IsDeleted = false,
            },
            new IdentityDocument()
            {
                Id = Guid.Parse("802425fa-d580-43a8-b0a4-3204c21bc9e7"),
                NameInDocument = "Harry Estes Michael",
                IsDeleted = false,
            },
        };

        private IMapper mapper;
        private IDeletableEntityRepository<IdentityDocument, Guid> repository;
        private IProfilesService profilesService;

        /// <summary>
        /// This method is called before every test.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        [SetUp]
        public async Task SetUp()
        {
            this.mapper = MapperMock.Instance;
            this.repository = DeletableEntityRepositoryMock<IdentityDocument, Guid>.Instance;

            foreach (var item in this.identityDocuments)
            {
                await this.repository.AddAsync(item);
            }

            this.profilesService = ProfilesServiceMock.Instance;
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
        /// This test check whether <see cref="IdentityDocumentsService.GetAllAsync(Common.QueryOptions{Dtos.IdentityDocument.IdentityDocumentDto}?)"/>
        /// returns all not deleted <see cref="IdentityDocument"/> entities.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAllAsync(QueryOptions<IdentityDocumentDto>? queryOptions = null)
        [Test]
        public async Task GetAllAsyncShouldReturnAllNotDeletedIdentityDocuments()
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);

            // Act
            var dtos = await service.GetAllAsync();

            // Assert
            var entities = this.repository.All(false, false).ToList();
            var names = entities.Select(e => e.NameInDocument);

            Assert.That(dtos, Has.Exactly(entities.Count).Items, "Entities count is not correct.");
            Assert.That(dtos.Any(rt => !names.Contains(rt.NameInDocument)), Is.False, "Entities are not correct.");
        }

        /// <summary>
        /// This test check whether <see cref="IdentityDocumentsService.GetAsync(Guid, Common.QueryOptions{Dtos.IdentityDocument.IdentityDocumentDto}?)"/>
        /// returns the correct identity document by a given id.
        /// </summary>
        /// <param name="id">The id of an existing identity document.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // GetAsync(Guid id, QueryOptions<IdentityDocumentDto>? queryOptions = null)
        [Test]
        [TestCase("a2c96289-6848-4f1c-92eb-31e88cf6b539")]
        public async Task GetAsyncShouldReturnExactIdentityDocument(string id)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var guid = Guid.Parse(id);

            // Act
            var dto = await service.GetAsync(guid);

            // Assert
            var entity = this.repository.Find(guid);

            Assert.That(dto.Id, Is.EqualTo(entity.Id), "Entity's id is not correct.");
            Assert.That(dto.NameInDocument, Is.EqualTo(entity.NameInDocument), "Entity's data is not correct.");
        }

        /// <summary>
        /// This test check whether <see cref="IdentityDocumentsService.GetAsync(Guid, Common.QueryOptions{Dtos.IdentityDocument.IdentityDocumentDto}?)"/>
        /// throws an exception when no identity document with a specified id exists.
        /// </summary>
        /// <param name="id">The id of a non existing identity document.</param>
        // GetAsync(Guid id, QueryOptions<IdentityDocumentDto>? queryOptions = null)
        [Test]
        [TestCase("61ce077d-9dd9-4a83-9037-953385e5e7c2")]
        public void GetAsyncShouldThrowWhenIdentityDocumentDoesNotExist(string id)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var guid = Guid.Parse(id);

            // Act
            var code = async () => await service.GetAsync(guid);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="IdentityDocumentsService.CreateAsync(IdentityDocumentDto)"/>
        /// throws an exception when the owner does not exist.
        /// </summary>
        /// <param name="ownerId">The id of a non existing guest profile.</param>
        // CreateAsync(IdentityDocumentDto identityDocumentDto)
        [Test]
        [TestCase("ea364fd9-9eab-4002-b34d-daf1453aa30d")]
        public void CreateAsyncShouldThrowWhenOwnerDoesNotExist(string ownerId)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var ownerGuid = Guid.Parse(ownerId);

            // Act
            var dto = new IdentityDocumentDto()
            {
                OwnerId = ownerGuid,
            };

            var code = async () => await service.CreateAsync(dto);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="IdentityDocumentsService.CreateAsync(IdentityDocumentDto)"/>
        /// throws an exception when the dto state is not valid.
        /// </summary>
        /// <param name="ownerId">The id of an existing guest profile.</param>
        // CreateAsync(IdentityDocumentDto identityDocumentDto)
        [Test]
        [TestCase("7844a439-b538-4245-819e-2d32bc472ecb")]
        public void CreateAsyncShouldThrowWhenDtoInNotValid(string ownerId)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var ownerGuid = Guid.Parse(ownerId);

            // Act
            var emptyDto = new IdentityDocumentDto()
            {
                OwnerId = ownerGuid,
            };
            var minDto = new IdentityDocumentDto()
            {
                OwnerId = ownerGuid,
                DocumentNumber = new string('*', NumberMinLength - 1),
            };
            var maxDto = new IdentityDocumentDto()
            {
                OwnerId = ownerGuid,
                DocumentNumber = new string('*', NumberMaxLength + 1),
            };

            var code = async (IdentityDocumentDto dto) => await service.CreateAsync(dto);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await code(emptyDto), "Method should throw an exception.");
            Assert.ThrowsAsync<ArgumentException>(async () => await code(minDto), "Method should throw an exception.");
            Assert.ThrowsAsync<ArgumentException>(async () => await code(maxDto), "Method should throw an exception.");
        }

        /// <summary>
        /// This test check whether <see cref="IdentityDocumentsService.CreateAsync(IdentityDocumentDto)"/>
        /// creates a new identity document entity and adds it to the database.
        /// </summary>
        /// <param name="ownerId">The id of an existing guest profile.</param>
        /// <param name="nameInDocument">The name in the document.</param>
        /// <param name="documentNumber">The number of the document.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // CreateAsync(IdentityDocumentDto identityDocumentDto)
        [Test]
        [TestCase("5155ac4a-d650-452f-8e77-040f17585634", "Georgi Kaloqnov Veselinov", "123456789")]
        public async Task CreateDetailedAsyncShouldCreateAndAddANewIdentityDocument(
            string ownerId,
            string nameInDocument,
            string documentNumber)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var guid = Guid.NewGuid();
            var ownerGuid = Guid.Parse(ownerId);

            // Act
            var dto = new IdentityDocumentDto()
            {
                Id = guid,
                OwnerId = ownerGuid,
                Type = IdentityDocumentType.Id,
                NameInDocument = nameInDocument,
                DocumentNumber = documentNumber,
                Country = "Bulgaria",
                Birthdate = DateOnly.FromDateTime(DateTime.Today).AddDays(-300),
                PlaceOfBirth = "Sofia",
                Nationality = "Bulgarian",
                ValidFrom = DateOnly.FromDateTime(DateTime.Today).AddDays(365 * -9),
                ValidUntil = DateOnly.FromDateTime(DateTime.Today).AddDays(365),
                IssuedBy = "MVR Sofia",
            };

            await service.CreateAsync(dto);

            // Assert
            var entity = this.repository.Find(guid);

            Assert.That(entity.Type, Is.EqualTo(IdentityDocumentType.Id), "Entity's document type is not correct.");
            Assert.That(entity.NameInDocument, Is.EqualTo(nameInDocument), "Entity's name in document is not correct.");
            Assert.That(entity.DocumentNumber, Is.EqualTo(documentNumber), "Entity's document number is not correct.");
            Assert.That(entity.Nationality, Is.EqualTo("Bulgarian"), "Entity's nationality is not correct.");
            Assert.That(entity.IssuedBy, Is.EqualTo("MVR Sofia"), "Entity's issued by is not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="IdentityDocumentsService.EditAsync(Guid, IdentityDocumentDto)"/>
        /// throws an exception when identity document does not exist.
        /// </summary>
        /// <param name="id">The id of a non existing identity document.</param>
        // EditAsync(Guid id, IdentityDocumentDto newIdentityDocument)
        [Test]
        [TestCase("f7b8dc97-7a2c-496d-8e9e-214ecefac87c")]
        public void EditAsyncShouldTheowWhenIdentityDocumentDoesNotExist(string id)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var guid = Guid.Parse(id);

            // Act
            var dto = new IdentityDocumentDto();

            var code = async () => await service.EditAsync(guid, dto);

            // Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
            Assert.That(exception.Message, Is.EqualTo("The document cannot be found."), "Exception message is not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="IdentityDocumentsService.EditAsync(Guid, IdentityDocumentDto)"/>
        /// throws an exception when the owner profile does not exist.
        /// </summary>
        /// <param name="id">The id of a existing identity document.</param>
        /// <param name="ownerId">The id of a non existing guest profile.</param>
        // EditAsync(Guid id, IdentityDocumentDto newIdentityDocument)
        [Test]
        [TestCase("802425fa-d580-43a8-b0a4-3204c21bc9e7", "b7b65ea6-1d0b-4f76-afd0-c4400adcc8eb")]
        public void EditAsyncShouldThrowWhenOwnerDoesNotExist(string id, string ownerId)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var guid = Guid.Parse(id);
            var ownerGuid = Guid.Parse(ownerId);

            // Act
            var dto = new IdentityDocumentDto()
            {
                OwnerId = ownerGuid,
            };

            var code = async () => await service.EditAsync(guid, dto);

            // Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
            Assert.That(exception.Message, Is.EqualTo("The new owner of the  document cannot be found."), "Exception message is not correct.");
        }

        /// <summary>
        /// This test checks whether <see cref="IdentityDocumentsService.EditAsync(Guid, IdentityDocumentDto)"/>
        /// throws an exception when the dto state is not valid.
        /// </summary>
        /// <param name="id">The id of an existing identity document.</param>
        /// <param name="ownerId">The id of an existing guest profile.</param>
        // EditAsync(Guid id, IdentityDocumentDto newIdentityDocument)
        [Test]
        [TestCase("d967bbec-6b55-46d7-a33f-ef896cfc3a8f", "7844a439-b538-4245-819e-2d32bc472ecb")]
        public void EditAsyncShouldThrowWhenDtoIsNotValid(string id, string ownerId)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var guid = Guid.Parse(id);
            var ownerGuid = Guid.Parse(ownerId);

            // Act
            var emptyDto = new IdentityDocumentDto()
            {
                OwnerId = ownerGuid,
            };
            var minDto = new IdentityDocumentDto()
            {
                OwnerId = ownerGuid,
                DocumentNumber = new string('*', NumberMinLength - 1),
            };
            var maxDto = new IdentityDocumentDto()
            {
                OwnerId = ownerGuid,
                DocumentNumber = new string('*', NumberMaxLength + 1),
            };

            var code = async (IdentityDocumentDto dto) => await service.EditAsync(guid, dto);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await code(emptyDto), "Method should throw an exception.");
            Assert.ThrowsAsync<ArgumentException>(async () => await code(minDto), "Method should throw an exception.");
            Assert.ThrowsAsync<ArgumentException>(async () => await code(maxDto), "Method should throw an exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="IdentityDocumentsService.EditAsync(Guid, IdentityDocumentDto)"/>
        /// modifies the entity's data.
        /// </summary>
        /// <param name="id">The id of the identity document to be modified.</param>
        /// <param name="ownerId">The new owner of the identity document.</param>
        /// <param name="nameInDocument">The new name in document.</param>
        /// <param name="documentNumber">The new document number.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // EditAsync(Guid id, IdentityDocumentDto newIdentityDocument)
        [Test]
        [TestCase(
            "802425fa-d580-43a8-b0a4-3204c21bc9e7",
            "7844a439-b538-4245-819e-2d32bc472ecb",
            "Vasil Georgiev Spasov",
            "987654321")]
        public async Task EditAsyncShouldModifyIdentityDocumentData(
            string id,
            string ownerId,
            string nameInDocument,
            string documentNumber)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var guid = Guid.Parse(id);
            var ownerGuid = Guid.Parse(ownerId);

            // Act
            var dto = new IdentityDocumentDto()
            {
                Id = guid,
                OwnerId = ownerGuid,
                Type = IdentityDocumentType.Passport,
                NameInDocument = nameInDocument,
                DocumentNumber = documentNumber,
                Country = "Germany",
                Birthdate = DateOnly.FromDateTime(DateTime.Today).AddDays(-300),
                PlaceOfBirth = "Berlin",
                Nationality = "German",
                ValidFrom = DateOnly.FromDateTime(DateTime.Today).AddDays(365 * -9),
                ValidUntil = DateOnly.FromDateTime(DateTime.Today).AddDays(365),
                IssuedBy = "Berlin",
            };

            await service.EditAsync(guid, dto);

            // Assert
            var entity = this.repository.Find(guid);

            Assert.That(entity.Type, Is.EqualTo(IdentityDocumentType.Passport), "Entity's document type is not modified.");
            Assert.That(entity.NameInDocument, Is.EqualTo(nameInDocument), "Entity's name in document is not modified.");
            Assert.That(entity.DocumentNumber, Is.EqualTo(documentNumber), "Entity's document number is not modified.");
            Assert.That(entity.Nationality, Is.EqualTo("German"), "Entity's nationality is not modified.");
            Assert.That(entity.IssuedBy, Is.EqualTo("Berlin"), "Entity's issued by is not modified.");
        }

        /// <summary>
        /// This test checks whether <see cref="IdentityDocumentsService.DeleteAsync(Guid)"/>
        /// throws an exception when identity document does not exist.
        /// </summary>
        /// <param name="id">The id of a non existing identity document.</param>
        // DeleteAsync(Guid id)
        [Test]
        [TestCase("79a4f68e-9f7d-4a3f-8996-f0f6fb1bc02a")]
        public void DeleteAsycnShouldThrowWhenIdentityDocumentDoesNotExist(string id)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var guid = Guid.Parse(id);

            // Act
            var code = async () => await service.DeleteAsync(guid);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Method should throw an exception.");
        }

        /// <summary>
        /// This test checks whether <see cref="IdentityDocumentsService.DeleteAsync(Guid)"/>
        /// deletes an existing identity document by setting its isDeleted flag to true.
        /// </summary>
        /// <param name="id">The id of an existing identity document.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // DeleteAsync(Guid id)
        [Test]
        [TestCase("a2c96289-6848-4f1c-92eb-31e88cf6b539")]
        public async Task DeleteAsyncShouldMarkIdentityDocumentAsDeleted(string id)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var guid = Guid.Parse(id);

            // Act
            await service.DeleteAsync(guid);

            // Assert
            var entity = this.repository.Find(guid);
            Assert.That(entity.IsDeleted, Is.True, "Entity IsDeleted flag is not set to true.");
        }

        /// <summary>
        /// This test checks whether <see cref="IdentityDocumentsService.ExistsAsync(Guid, Common.QueryOptions{IdentityDocumentDto}?)"/>
        /// returns <see langword="true"/> for an existing identity document.
        /// </summary>
        /// <param name="id">The id of an existing identity document.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // ExistsAsync(Guid id, QueryOptions<IdentityDocumentDto>? queryOptions = null)
        [Test]
        [TestCase("a2c96289-6848-4f1c-92eb-31e88cf6b539")]
        public async Task ExistAsyncShouldReturnTrueForExistingIdentityDocument(string id)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var guid = Guid.Parse(id);

            // Act
            var result = await service.ExistsAsync(guid);

            // Assert
            Assert.That(result, Is.True, "Result should be true.");
        }

        /// <summary>
        /// This test checks whether <see cref="IdentityDocumentsService.ExistsAsync(Guid, Common.QueryOptions{IdentityDocumentDto}?)"/>
        /// returns <see langword="false"/> for a non existing identity document.
        /// </summary>
        /// <param name="id">The id of a non existing identity document.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // ExistsAsync(Guid id, QueryOptions<IdentityDocumentDto>? queryOptions = null)
        [Test]
        [TestCase("1817a405-9230-41a2-a03f-e04a2ad79d46")]
        public async Task ExistAsyncShouldReturnFalseForNonExistingIdentityDocument(string id)
        {
            // Arrange
            var service = new IdentityDocumentsService(this.repository, this.profilesService, this.mapper);
            var guid = Guid.Parse(id);

            // Act
            var result = await service.ExistsAsync(guid);

            // Assert
            Assert.That(result, Is.False, "Result should be false.");
        }
    }
}
