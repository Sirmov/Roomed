// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomTypesService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.RoomType;

    /// <summary>
    /// This class is a implementation of the <see cref="IRoomTypesService"/> interface.
    /// It's purpose is to abstract and encapsulate the business logic related to the <see cref="RoomType"/> entity.
    /// </summary>
    public class RoomTypesService : BaseService<RoomType, int>, IRoomTypesService
    {
        private readonly IDeletableEntityRepository<RoomType, int> roomTypesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomTypesService"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="entityRepository">The implementation of <see cref="IDeletableEntityRepository{TEntity, TKey}"/>.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        public RoomTypesService(
            IDeletableEntityRepository<RoomType, int> entityRepository,
            IMapper mapper)
            : base(entityRepository, mapper)
        {
            this.roomTypesRepository = entityRepository;
        }

        /// <inheritdoc/>
        public async Task<ICollection<RoomTypeDto>> GetAllAsync(QueryOptions<RoomTypeDto>? queryOptions = null)
        {
            return await base.GetAllAsync(queryOptions ?? new ());
        }

        /// <inheritdoc/>
        public async Task<RoomTypeDto> GetAsync(int id, QueryOptions<RoomTypeDto>? queryOptions = null)
        {
            return await base.GetAsync(id, queryOptions ?? new ());
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(int id, QueryOptions<RoomTypeDto>? queryOptions = null)
        {
            var result = true;

            try
            {
                await this.roomTypesRepository.FindAsync(id);
            }
            catch (InvalidOperationException)
            {
                result = false;
            }

            return result;
        }
    }
}
