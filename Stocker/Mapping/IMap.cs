namespace Mapping
{
    public interface IMap<TSource, TDestination>
    where TDestination : new()
    {
        TDestination Map(TSource source);
    }

    public interface IMap<TSource1, TSource2, TDestination>
    where TDestination : new()
    {
        TDestination Map(TSource1 source1, TSource2 source2);
    }
}