using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts;

public class
    GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;


    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();

        List<GetAllProductsQueryResponse> responses = new(); // interfaces'ler newlenemez
        foreach (var product in products)
            responses.Add(new GetAllProductsQueryResponse()
            {
                Title = product.Title,
                Description = product.Description,
                Discount = product.Discount,
                Price = product.Price - (product.Price * product.Discount / 100)
            });


        return responses;
    }
}