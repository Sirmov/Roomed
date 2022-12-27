// |-----------------------------------------------------------------------------------------------------|
// <copyright file="MapperMock.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Tests.Common
{
    using System.Reflection;

    using AutoMapper;

    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This class is a mock of <see cref="IMapper"/>.
    /// </summary>
    public static class MapperMock
    {
        /// <summary>
        /// Gets the <see cref="IMapper"/> instance of the mock.
        /// </summary>
        public static IMapper Instance
        {
            get
            {
                var asseblies = new Assembly[]
                {
                    typeof(ReservationDto).GetTypeInfo().Assembly,
                };
                AutoMapperConfig.RegisterMappings(asseblies);

                var mapper = AutoMapperConfig.MapperInstance;
                return mapper;
            }
        }
    }
}
