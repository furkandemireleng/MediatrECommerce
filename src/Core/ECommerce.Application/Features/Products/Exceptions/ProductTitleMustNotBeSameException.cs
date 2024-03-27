using ECommerce.Application.Base;

namespace ECommerce.Application.Features.Products.Exceptions;

public class ProductTitleMustNotBeSameException : BaseException
{
    public ProductTitleMustNotBeSameException() : base("F002") { }
}