using MediatR;

namespace ECommerce.Application.Features.Auth.Command.RefreshToken;

public class RefreshTokenCommandRequest : IRequest<RefreshTokenCommandResponse>
{
    public string AccessToken { get; set; }
    
    public string RefresToken { get; set; }
}