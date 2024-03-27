using FluentValidation;

namespace ECommerce.Application.Features.Products.Command.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .WithName("Name");

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .WithName("Description");


        RuleFor(x => x.BrandId)
            .NotEmpty()
            .NotNull()
            .WithName("Brand")
            .NotEqual(Guid.Empty);


        RuleFor(x => x.Price)
            .NotEqual(0)
            .NotNull()
            .WithName("Price")
            .NotEmpty();

        RuleFor(x => x.Discount)
            .GreaterThanOrEqualTo(0)
            .WithName("Discount");

        RuleFor(x => x.CategoryIds)
            .NotEmpty()
            .Must(cat => cat.Any())
            .WithName("Category");
    }
}