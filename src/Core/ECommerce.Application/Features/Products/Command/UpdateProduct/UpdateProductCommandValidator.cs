using FluentValidation;

namespace ECommerce.Application.Features.Products.Command.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithName("Id");


        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .WithName("Title");

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