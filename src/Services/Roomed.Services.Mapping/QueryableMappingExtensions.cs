// |-----------------------------------------------------------------------------------------------------|
// <copyright file="QueryableMappingExtensions.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Mapping
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    /// <summary>
    /// This class contains <see cref="IQueryable"/> mapping extension methods.
    /// </summary>
    public static class QueryableMappingExtensions
    {
        /// <summary>
        /// This method checks if the source is <see langword="null"/>
        /// and calls the <see cref="IMapper.ProjectTo{TDestination}(IQueryable, object, Expression{Func{TDestination, object}}[])"/> method.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination class.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/> source.</param>
        /// <param name="membersToExpand">The explicit members to expand.</param>
        /// <returns>Returns the <see cref="IQueryable{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws when <paramref name="source"/> is <see langword="null"/>.</exception>
        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo(AutoMapperConfig.MapperInstance.ConfigurationProvider, null, membersToExpand);
        }

        /// <summary>
        /// This method checks if the source is <see langword="null"/>
        /// and calls the <see cref="IMapper.ProjectTo{TDestination}(IQueryable, IDictionary{string, object}, string[])"/> method.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination class.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/> source.</param>
        /// <param name="parameters">The object containing the parameters.</param>
        /// <returns>Returns the <see cref="IQueryable{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws when <paramref name="source"/> is <see langword="null"/>.</exception>
        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            object parameters)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo<TDestination>(AutoMapperConfig.MapperInstance.ConfigurationProvider, parameters);
        }
    }
}
