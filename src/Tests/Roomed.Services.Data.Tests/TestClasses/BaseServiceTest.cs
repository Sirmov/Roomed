// |-----------------------------------------------------------------------------------------------------|
// <copyright file="BaseServiceTest.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Tests.TestClasses
{
    using AutoMapper;

    using Roomed.Data.Common.Models;
    using Roomed.Data.Common.Repositories;
    using Roomed.Services.Data.Common;

    public class BaseServiceTest<TEntity, TKey> : BaseService<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        public BaseServiceTest(IDeletableEntityRepository<TEntity, TKey> entityRepository, IMapper mapper)
            : base(entityRepository, mapper)
        {
        }

        public bool ValidateDto<TDto>(TDto dto)
        {
            return base.ValidateDto(dto);
        }
    }
}
