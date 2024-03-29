using ECommerce.Application.Base;
using ECommerce.Application.Interfaces.AutoMapper;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Features.Products.Command.UpdateProduct;

public class UpdateProductCommandHandler : BaseHandler,IRequestHandler<UpdateProductCommandRequest,Unit>
{
    // private readonly IUnitOfWork _unitOfWork;
    // private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor):base(mapper,unitOfWork,httpContextAccessor)
    {
        // _unitOfWork = unitOfWork;
        // _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDelete);

        var map = _mapper.Map<Product, UpdateProductCommandRequest>(request);

        var productCategories = await _unitOfWork.GetReadRepository<ProductCategory>()
            .GetAllAsync(x => x.ProductId == product.Id);

        await _unitOfWork.GetWriteRepository<ProductCategory>().HardDeleteRangeAsync(productCategories);

        foreach (var categoryId in request.CategoryIds)
        {
            await _unitOfWork.GetWriteRepository<ProductCategory>()
                .AddAsync(new() { CategoryId = categoryId, ProductId = product.Id });
        }

        await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
    
    
    
}