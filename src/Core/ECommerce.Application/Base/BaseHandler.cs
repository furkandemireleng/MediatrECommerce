using System.Security.Claims;
using ECommerce.Application.Interfaces.AutoMapper;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Base;

public class BaseHandler
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IHttpContextAccessor _httpContextAccessor;
    public readonly IMapper _mapper;
    public readonly string _userId;

    public BaseHandler(IMapper mapper, IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }


}