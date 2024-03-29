using ECommerce.Application.Base;
using ECommerce.Application.Interfaces.AutoMapper;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Features.Products.Command.DeleteProduct;

public class DeleteProductCommandHandler: BaseHandler,IRequestHandler<DeleteProductCommandRequest,Unit>
{
    // private readonly IUnitOfWork _unitOfWork;
    // private readonly IMapper _mapper;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper,unitOfWork,httpContextAccessor)
    {
        // _unitOfWork = unitOfWork;
        // _mapper = mapper;
    }
    public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDelete);
        product.IsDelete = true;

        await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
        await _unitOfWork.SaveAsync();

        return Unit.Value;

    }
}