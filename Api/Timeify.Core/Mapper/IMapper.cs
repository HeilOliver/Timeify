namespace Timeify.Core.Mapper
{
    public interface IMapper
    {
        TTo MapFrom<TFrom, TTo>(TFrom obj);

        void Map<TFrom, TTo>(TFrom from, TTo to);
    }
}