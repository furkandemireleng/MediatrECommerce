using FluentValidation;

namespace ECommerce.Application.Features.Products.Command.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithName("Id");
    }
}