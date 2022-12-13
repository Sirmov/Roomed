namespace Roomed.Tests.Common
{
    using System.Reflection;

    using AutoMapper;

    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Services.Mapping;

    public static class MapperMock
    {
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
