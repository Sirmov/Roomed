namespace Roomed.Services.Data.Contracts
{
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Dtos.ReservationDay;

    public interface IReservationDaysService
    {
        public Task<ICollection<ReservationDayDto>> GetAllForDate(DateOnly date, QueryOptions<ReservationDayDto>? queryOptions);

        public Task<ICollection<ReservationDayDto>> GetAllForPeriod(DateOnly startDate, DateOnly endDate, QueryOptions<ReservationDayDto>? queryOptions);
    }
}
