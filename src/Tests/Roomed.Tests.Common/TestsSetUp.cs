namespace Roomed.Tests.Common
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Roomed.Data;
    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Services.Mapping;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class TestsSetUp
    {
        private static ApplicationDbContext DbContext;

        private static readonly ICollection<ReservationNote> reservationNotes = new List<ReservationNote>
        {
            new ReservationNote()
            {
                Id = Guid.Parse("2bfff802-5afb-4bbb-96b3-27c98161ff00"),
                Body = "Reservation note #1",
                IsDeleted = false,
            },
            new ReservationNote()
            {
                Id = Guid.Parse("bb2f7b6c-d8d9-4e2c-b14b-3bd98e18ad86"),
                Body = "Reservation note #2",
                IsDeleted = true,
            },
            new ReservationNote()
            {
                Id = Guid.Parse("08bd1b0d-15fd-4d2e-9f59-979d09da1133"),
                Body = "Reservation note #3",
                IsDeleted = false,
            },
        };

        public static async Task<ApplicationDbContext> InitializeDbContextAsync()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("RoomedInMemory")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            if (dbContext != null)
            {
                await dbContext.Database.EnsureDeletedAsync();
            }

            await dbContext.AddRangeAsync(reservationNotes);
            await dbContext.SaveChangesAsync();

            DbContext = dbContext;
            return dbContext;
        }

        public static IMapper GetMapper()
        {
            var asseblies = new Assembly[]
            {
                typeof(ReservationDto).GetTypeInfo().Assembly,
            };
            AutoMapperConfig.RegisterMappings(asseblies);

            var mapper = AutoMapperConfig.MapperInstance;
            return mapper;
        }

        public static IDeletableEntityRepository<ReservationNote, Guid> GetReservationNotesRepository()
        {
            var mock = new Mock<IDeletableEntityRepository<ReservationNote, Guid>>();
            mock.Setup(m => m.All(It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns((bool isReadonly, bool withDeleted) =>
                {
                    IQueryable<ReservationNote> query = DbContext.Set<ReservationNote>();

                    if (isReadonly)
                    {
                        query = query.AsNoTracking();
                    }

                    if (!withDeleted)
                    {
                        query = query.Where(e => !e.IsDeleted);
                    }

                    return query;
                });

            //mock.Setup(m => m.FindAsync(It.IsAny<Guid>(), It.IsAny<bool>()).Result)
            //    .Returns(async (Guid id, bool isReadonly) => await DbContext.Set<ReservationNote>().FindAsync(id));

            var repository = mock.Object;
            return repository;
        }
    }
}