using MediatR;

namespace ECommerce.Application.Features.Products.Command.DeleteProduct;

public class DeleteProductCommandRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}