using ECommerce.Application.Features.Brands.Commands.CreateBrand;
using ECommerce.Application.Features.Brands.Queries;
using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BrandController : ControllerBase
{
    private readonly IMediator _mediator;


    public BrandController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBrands()
    {
        var response = await _mediator.Send(new GetAllBrandsQueryRequest());

        return Ok(response);
        //throw new Exception();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBrandCommandRequest request)
    {
        await _mediator.Send(request);

        return Ok();
        //throw new Exception();
    }
}