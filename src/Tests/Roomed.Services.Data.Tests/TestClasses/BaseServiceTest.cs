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

    /// <summary>
    /// This is a test class of <see cref="BaseService{TEntity, TKey}"/> used for testing purposes.
    /// </summary>
    /// <typeparam name="TEntity">The data model entity.</typeparam>
    /// <typeparam name="TKey">The type of the id of the <typeparamref name="TEntity"/>.</typeparam>
    public class BaseServiceTest<TEntity, TKey> : BaseService<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseServiceTest{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="entityRepository">The repository of the <typeparamref name="TEntity"/>.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        public BaseServiceTest(IDeletableEntityRepository<TEntity, TKey> entityRepository, IMapper mapper)
            : base(entityRepository, mapper)
        {
        }

        /// <summary>
        /// This method exposes the <see langword="protected"/> <see cref="BaseService{TEntity, TKey}.ValidateDto{TDto}(TDto)"/>.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="dto">The dto to be validated.</param>
        /// <returns>Returns a <see cref="bool"/> indicating whether the dto is valid.</returns>
        public new bool ValidateDto<TDto>(TDto dto)
        {
            return base.ValidateDto(dto);
        }
    }
}
