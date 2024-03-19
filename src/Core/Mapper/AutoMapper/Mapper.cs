using AutoMapper;
using AutoMapper.Internal;

namespace Mapper.AutoMapper;

public class Mapper : ECommerce.Application.Interfaces.AutoMapper.IMapper
{
    public static List<TypePair> typePairs = new();

    private IMapper MapperContainer;


    protected void Config<TDestination, TSource>(int depth, string ignore = null)
    {
        //depth auto mapper'da default 5 ama configure edilebilir
        //dto icinde dto icinde dto gibi

        var typePair = new TypePair(typeof(TSource), typeof(TDestination));
        if (typePairs.Any(a =>
                a.DestinationType == typePair.DestinationType && a.SourceType == typePair.SourceType && ignore is null))
            return;

        typePairs.Add(typePair);

        var config = new MapperConfiguration(cfg =>
        {
            foreach (var item in typePairs)
            {
                if (ignore is null)
                {
                    cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth)
                        .ForMember(ignore, x => x.Ignore()).ReverseMap();
                }
                else
                {
                    cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ReverseMap();
                }
            }
        });

        MapperContainer = config.CreateMapper();
    }


    public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
    {
        Config<TDestination, TSource>(5, ignore);

        return MapperContainer.Map<TSource, TDestination>(source);
    }

    public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignore = null)
    {
        Config<TDestination, TSource>(5, ignore);

        return MapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
    }


    public TDestination Map<TDestination, TSource>(object source, string? ignore = null)
    {
        Config<TDestination, object>(5, ignore);

        return MapperContainer.Map<object, TDestination>(source);
    }

    public IList<TDestination> Map<TDestination, TSource>(IList<object> source, string? ignore = null)
    {
        Config<TDestination, object>(5, ignore);

        return MapperContainer.Map<IList<object>, IList<TDestination>>(source);
    }
}