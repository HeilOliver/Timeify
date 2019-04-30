using Timeify.Common.DI;
using Timeify.Core.Mapper;

namespace Timeify.Infrastructure.Mapper
{
    [Injectable(typeof(IMapper), InjectableAttribute.LifeTimeType.Container)]
    public class Mapper : IMapper
    {
        private readonly AutoMapper.IMapper mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TTo MapFrom<TFrom, TTo>(TFrom obj)
        {
            return mapper.Map<TFrom, TTo>(obj);
        }

        public void Map<TFrom, TTo>(TFrom from, TTo to)
        {
            mapper.Map(from, to);
        }
    }
}