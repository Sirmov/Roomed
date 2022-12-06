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
