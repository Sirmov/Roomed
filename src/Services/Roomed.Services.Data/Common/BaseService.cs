// |-----------------------------------------------------------------------------------------------------|
// <copyright file="BaseService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Common.Models;
    using Roomed.Data.Common.Repositories;
    using Roomed.Services.Data.Contracts;

    /// <summary>
    /// This is a base class for all services.
    /// It adds the support for modifying queries using <see cref="QueryOptions{TDto}"/> parameter.
    /// </summary>
    /// <typeparam name="TEntity">The data model entity.</typeparam>
    /// <typeparam name="TKey">The type of the id of the <typeparamref name="TEntity"/>.</typeparam>
    public class BaseService<TEntity, TKey> : IBaseService<TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        /// <summary>
        /// This field contains an implementation of <see cref="IMapper"/>.
        /// </summary>
        protected readonly IMapper mapper;

        private readonly IDeletableEntityRepository<TEntity, TKey> entityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="entityRepository">The repository of the <typeparamref name="TEntity"/>.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        public BaseService(IDeletableEntityRepository<TEntity, TKey> entityRepository, IMapper mapper)
        {
            this.entityRepository = entityRepository;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public virtual async Task<ICollection<TDto>> GetAllAsync<TDto>(QueryOptions<TDto>? queryOptions = null)
        {
            var query = this.entityRepository
                .All(queryOptions?.IsReadOnly ?? false, queryOptions?.WithDeleted ?? false)
                .ProjectTo<TDto>(this.mapper.ConfigurationProvider);

            this.ModifyQuery(query, queryOptions ?? new ());

            var dtos = await query.ToListAsync();

            return dtos;
        }

        /// <inheritdoc/>
        public virtual async Task<TDto> GetAsync<TDto>(TKey id, QueryOptions<TDto>? queryOptions = null)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await this.entityRepository.FindAsync(id);

            var dto = mapper.Map<TDto>(entity);

            return dto;
        }

        /// <summary>
        /// This method modifies the query based on the <see cref="QueryOptions{TDto}"/> passed.
        /// </summary>
        /// <typeparam name="TDto">The type of the class returned by the query.</typeparam>
        /// <param name="query">The original query.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns the modified query.</returns>
        protected IQueryable<TDto> ModifyQuery<TDto>(IQueryable<TDto> query, QueryOptions<TDto> queryOptions)
        {
            if (queryOptions == null)
            {
                return query;
            }

            foreach (var orderOption in queryOptions.OrderOptions)
            {
                if (orderOption.Order == OrderByOrder.Ascending)
                {
                    query = query.OrderBy(x => this.GetPropertyInfo(x, orderOption.Property).GetValue(x));
                }
                else
                {
                    query = query.OrderByDescending(x => this.GetPropertyInfo(x, orderOption.Property).GetValue(x));
                }
            }

            if (queryOptions.Skip.HasValue)
            {
                query = query.Skip(queryOptions.Skip.Value);
            }

            if (queryOptions.Take.HasValue)
            {
                query = query.Take(queryOptions.Take.Value);
            }

            return query;
        }

        /// <summary>
        /// This method initializes a new <see cref="ValidationContext"/> and by using <see cref="Validator"/>
        /// determines whether a dto is valid by looking at his validation attributes.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="dto">The dto to be validated.</param>
        /// <returns>Returns a <see cref="bool"/> indicating whether the dto is valid.</returns>
        /// <exception cref="ArgumentNullException">Throws when <paramref name="dto"/> is null.</exception>
        protected bool ValidateDto<TDto>(TDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var context = new ValidationContext(dto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, context, validationResults, true);

            return isValid;
        }

        private PropertyInfo GetPropertyInfo<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);
            MemberExpression? member = propertyLambda.Body as MemberExpression;

            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));
            }

            PropertyInfo? propInfo = member.Member as PropertyInfo;

            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));
            }

            return propInfo;
        }
    }
}
