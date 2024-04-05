using ECommerce.Application.Features.Products.Command.CreateProduct;
using ECommerce.Application.Features.Products.Command.DeleteProduct;
using ECommerce.Application.Features.Products.Command.UpdateProduct;
using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;


    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var response = await _mediator.Send(new GetAllProductsQueryRequest());

        return Ok(response);
        //throw new Exception();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }
}