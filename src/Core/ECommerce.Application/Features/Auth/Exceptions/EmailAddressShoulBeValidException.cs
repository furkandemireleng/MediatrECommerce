using ECommerce.Application.Base;

namespace ECommerce.Application.Features.Auth.Exceptions;

public class EmailAddressShoulBeValidException: BaseException
{
    public EmailAddressShoulBeValidException(): base("U005")
    {
    }
}