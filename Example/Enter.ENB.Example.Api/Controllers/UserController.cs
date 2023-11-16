using Enter.ENB.AspNetCore.Mvc;
using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Enter.ENB.Example.Api.Controllers;

[Route("Api/Users")]
public class UserController : EntCrudControllerBase<EntUserDto,Guid,PagedAndSortedResultRequestDto,UserCreateDto,UserUpdateDto>
{
    public UserController(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }
}