using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ECommerce.Application.Base;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Interfaces.AutoMapper;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Application.Interfaces.Tokens;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Features.Auth.Command.RefreshToken;

public class RefreshTokenCommandHandler : BaseHandler,
    IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
{
    private readonly AuthRules _authRules;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;


    public RefreshTokenCommandHandler(AuthRules authRules, UserManager<User> userManager, ITokenService tokenService,
        IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        : base(mapper, unitOfWork, httpContextAccessor)
    {
        _authRules = authRules;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request,
        CancellationToken cancellationToken)
    {
        ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        string email = principal.FindFirstValue(ClaimTypes.Email);

        User? user = await _userManager.FindByEmailAsync(email);
        IList<string> roles = await _userManager.GetRolesAsync(user);

        await _authRules.RefreshTokenShouldNotExpired(user.RefreshTokenExpireTime);


        JwtSecurityToken newAccessToken = await _tokenService.CreateToken(user, roles);
        string newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefresToken = newRefreshToken
        };


    }
}