// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomsServiceMock.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Tests.Common
{
    using AutoMapper;
    using Moq;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Room;

    /// <summary>
    /// This class is a mock of <see cref="IRoomsService"/>.
    /// </summary>
    public static class RoomsServiceMock
    {
        private static readonly ICollection<Room> Rooms = new List<Room>()
        {
            new Room()
            {
                Id = 1,
                Number = "100",
                IsDeleted = false,
            },
            new Room()
            {
                Id = 2,
                Number = "101",
                IsDeleted = false,
            },
            new Room()
            {
                Id = 3,
                Number = "200",
                IsDeleted = false,
            },
            new Room()
            {
                Id = 4,
                Number = "201",
                IsDeleted = false,
            },
        }.AsReadOnly();

        private static IMapper mapper = MapperMock.Instance;
        private static IDeletableEntityRepository<Room, int> roomsRepository = DeletableEntityRepositoryMock<Room, int>.Instance;

        private static bool isInitialized = false;

        /// <summary>
        /// Gets the <see cref="IRoomsService"/> instance of the mock.
        /// </summary>
        public static IRoomsService Instance
        {
            get
            {
                if (!isInitialized)
                {
                    foreach (var item in Rooms)
                    {
                        roomsRepository.Add(item);
                    }
                }

                var mock = new Mock<IRoomsService>();

                mock.Setup(m => m.ExistsAsync(It.IsAny<int>(), It.IsAny<QueryOptions<RoomDto>?>()).Result)
                    .Returns((int id, QueryOptions<RoomDto>? queryOptions) =>
                    {
                        var result = true;

                        try
                        {
                            roomsRepository.Find(id);
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
