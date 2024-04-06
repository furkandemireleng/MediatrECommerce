using Bogus;
using ECommerce.Application.Base;
using ECommerce.Application.Interfaces.AutoMapper;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandHandler : BaseHandler, IRequestHandler<CreateBrandCommandRequest, Unit>
{
    public CreateBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) :
        base(mapper, unitOfWork, httpContextAccessor)
    {
    }

    public async Task<Unit> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
    {
        Faker faker = new("en");

        List<Brand> brands = new();

        for (int i = 0; i < 1000000; i++)
        {
            brands.Add(new Brand(faker.Commerce.Department()));
        }

        await _unitOfWork.GetWriteRepository<Brand>().AddRangeAsync(brands);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}