using ECommerce.Application.Interfaces.RedisCache;
using MediatR;

namespace ECommerce.Application.Features.Brands.Queries;

public class GetAllBrandsQueryRequest : IRequest<IList<GetAllBrandsQueryResponse>>, ICacheableQuery
{
    public string CacheKey => "GetAllBrands";

    public double CacheTime => 5;
}