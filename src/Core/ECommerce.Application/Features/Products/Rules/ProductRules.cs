using ECommerce.Application.Base;
using ECommerce.Application.Features.Products.Exceptions;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Products.Rules;

public class ProductRules : BaseRules
{
    public Task ProductTitleMustNotBeSame(IList<Product> products, string productTitle)
    {
        if (products.Any(x => x.Title == productTitle))
        {
            throw new ProductTitleMustNotBeSameException();
        }

        return Task.CompletedTask;
    }
}