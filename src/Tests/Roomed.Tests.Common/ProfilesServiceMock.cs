// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfilesServiceMock.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Tests.Common
{
    using AutoMapper;
    using Moq;

    using Roomed.Data.Common.Repositories;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Profile;

    using Profile = Roomed.Data.Models.Profile;

    /// <summary>
    /// This class is a mock of <see cref="IProfilesService"/>.
    /// </summary>
    public static class ProfilesServiceMock
    {
        private static readonly ICollection<Profile> Profiles = new List<Profile>()
        {
            new Profile()
            {
                Id = Guid.Parse("7844a439-b538-4245-819e-2d32bc472ecb"),
                FirstName = "Annika",
                LastName = "Fleming",
                IsDeleted = false,
            },
            new Profile()
            {
                Id = Guid.Parse("5155ac4a-d650-452f-8e77-040f17585634"),
                FirstName = "Tim",
                LastName = "Wright",
                IsDeleted = false,
            },
        }.AsReadOnly();

        private static IMapper mapper = MapperMock.Instance;
        private static IDeletableEntityRepository<Profile, Guid> profilesRepository = DeletableEntityRepositoryMock<Profile, Guid>.Instance;

        private static bool isInitialized = false;

        /// <summary>
        /// Gets the <see cref="IProfilesService"/> instance of the mock.
        /// </summary>
        public static IProfilesService Instance
        {
            get
            {
                if (!isInitialized)
                {
                    foreach (var item in Profiles)
                    {
                        profilesRepository.Add(item);
                    }
                }

                var mock = new Mock<IProfilesService>();

                mock.Setup(m => m.ExistsAsync(It.IsAny<Guid>(), It.IsAny<QueryOptions<DetailedProfileDto>?>()).Result)
                    .Returns((Guid id, QueryOptions<DetailedProfileDto>? queryOptions) =>
                    {
                        var result = true;

                        try
                        {
                            profilesRepository.Find(id);
                        }
                        catch (InvalidOperationException)
                        {
                            result = false;
                        }

                        return result;
                    });

                var service = mock.Object;
                return service;
            }
        }
    }
}
