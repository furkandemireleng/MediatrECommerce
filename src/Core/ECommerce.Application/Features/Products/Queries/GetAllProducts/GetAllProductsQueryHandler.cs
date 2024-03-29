using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces.AutoMapper;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts;

public class
    GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.GetReadRepository<Product>()
            .GetAllAsync(p => p.IsDelete == false,
                include: x => x.Include(b => b.Brand));

        var brand = _mapper.Map<BrandDto, Brand>(new Brand());


        var map = _mapper.Map<GetAllProductsQueryResponse, Product>(products);

        foreach (var item in map)
        {
            item.Price -= (item.Price * item.Discount / 100);
        }

        return map;
    }
}