using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using ECommerce.Application.Base;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Interfaces.AutoMapper;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Application.Interfaces.Tokens;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Application.Features.Auth.Command.Login;

public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
{
    private readonly IConfiguration _configuration;
    private readonly AuthRules _authRules;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IConfiguration configuration, AuthRules authRules, UserManager<User> userManager,
        ITokenService tokenService, IMapper mapper, IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
        _configuration = configuration;
        _authRules = authRules;
        _userManager = userManager;
        _tokenService = tokenService;
    }


    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByEmailAsync(request.Email);
        bool checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        await _authRules.EmailOrPasswordBeInvalid(user, checkPassword);

        IList<string> roles = await _userManager.GetRolesAsync(user);

        JwtSecurityToken token = await _tokenService.CreateToken(user, roles);
        string refreshToken = _tokenService.GenerateRefreshToken();

        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays);
        


        await _userManager.UpdateAsync(user);

        await _userManager.UpdateSecurityStampAsync(user);

        string _token = new JwtSecurityTokenHandler().WriteToken(token);

        await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

        return new()
        {
            Token = _token,
            RefreshToken = refreshToken,
            ExpireTime = token.ValidTo
        };
    }
}