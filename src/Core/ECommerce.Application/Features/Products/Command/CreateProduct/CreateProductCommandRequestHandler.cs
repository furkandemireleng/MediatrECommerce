using ECommerce.Application.Features.Products.Rules;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Products.Command.CreateProduct;

public class CreateProductCommandRequestHandler : IRequestHandler<CreateProductCommandRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProductRules _productRules;

    public CreateProductCommandRequestHandler(IUnitOfWork unitOfWork, ProductRules productRules)
    {
        _unitOfWork = unitOfWork;
        _productRules = productRules;
    }

    public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        IList<Product> products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();


        await _productRules.ProductTitleMustNotBeSame(products, request.Title);


        Product product = new(request.Title, request.Description, request.Price, request.Discount,
            request.BrandId);

        await _unitOfWork.GetWriteRepository<Product>().AddAsync(product);

        if (await _unitOfWork.SaveAsync() > 0)
        {
            foreach (var categoryId in request.CategoryIds)
            {
                await _unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new()
                {
                    CategoryId = categoryId,
                    ProductId = product.Id
                });

                await _unitOfWork.SaveAsync();
            }
        }

        return Unit.Value;
    }
}