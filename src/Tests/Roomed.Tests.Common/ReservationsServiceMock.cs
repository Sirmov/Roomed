namespace Roomed.Tests.Common
{
    using AutoMapper;
    using Moq;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Reservation;

    /// <summary>
    /// This class is a mock of <see cref="IReservationsService"/>.
    /// </summary>
    public static class ReservationsServiceMock
    {
        private static readonly ICollection<Reservation> Reservations = new List<Reservation>()
        {
            new Reservation()
            {
                Id = Guid.Parse("a0368b88-05bb-48ff-83cb-0c1c6a323e4e"),
            },
            new Reservation()
            {
                Id = Guid.Parse("33fec2d6-a85c-40f8-8738-848c68bc2ac8"),
            },
        }.AsReadOnly();

        private static IMapper mapper = MapperMock.Instance;
        private static IDeletableEntityRepository<Reservation, Guid> reservationsRepository = DeletableEntityRepositoryMock<Reservation, Guid>.Instance;

        private static bool isInitialized = false;

        /// <summary>
        /// Gets the <see cref="IReservationsService"/> instance of the mock.
        /// </summary>
        public static IReservationsService Instance
        {
            get
            {
                if (!isInitialized)
                {
                    foreach (var item in Reservations)
                    {
                        reservationsRepository.Add(item);
                    }
                }

                var mock = new Mock<IReservationsService>();

                mock.Setup(m => m.ExistsAsync(It.IsAny<Guid>(), It.IsAny<QueryOptions<ReservationDto>?>()).Result)
                    .Returns((Guid id, QueryOptions<ReservationDto>? queryOptions) =>
                    {
                        var result = true;

                        try
                        {
                            reservationsRepository.Find(id);
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
