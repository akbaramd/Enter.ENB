using Enter.ENB.AspNetCore.Mvc;
using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Enter.ENB.Identity.Api.Controllers;

[Route("Api/Users")]
public class UserController : EntControllerBase
{
    public UserController(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }
    
    public IEntIdentityUserAppService CrudService 
        => LazyServiceProvider.GetRequiredService<IEntIdentityUserAppService>();


    [HttpGet("Filter")]
    public async Task<PagedResultDto<EntIdentityUserDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
    {
        return await CrudService.GetListAsync(input);
    }
    
    [HttpPost("Create")]
    public async Task<EntIdentityUserDto> Create([FromBody] EntIdentityUserCreateDto input)
    {
        return await CrudService.CreateAsync(input);
    }
    
    
    [HttpGet("{userID}/Details")]
    public async Task<EntIdentityUserDto> GetAsync(Guid userID)
    {
        return await CrudService.GetAsync(userID);
    }
    
    [HttpPut("{userID}/Update")]
    public async Task<EntIdentityUserDto> Update(Guid userID, [FromBody] EntIdentityUserUpdateDto input)
    {
        var res = await CrudService.UpdateAsync(userID,input);
        return res;
    }
    [HttpDelete("{userID}/Delete")]
    public async Task<ActionResult> Delete(Guid userID)
    {
        await CrudService.DeleteAsync(userID);
        return Ok();
    }
    
    
    [HttpPut("{userId}/Roles/Assign")]
    public async Task<ActionResult> AssignRole(Guid userId,[FromBody] EntIdentityUserAssignRoleDto input)
    {
        await CrudService.AssignRoleAsync(userId,input);
        return Ok();
    }
    
    [HttpPut("{userId}/Roles/UnAssign")]
    public async Task<ActionResult> UnAssignRole(Guid userId,[FromBody] EntIdentityUserUnAssignRoleDto input)
    {
        await CrudService.UnAssignRoleAsync(userId,input);
        return Ok();
    }
    
    [HttpPost("{userId}/Roles/Exists")]
    public async Task<ActionResult> ExistsRole(Guid userId,[FromBody] EntIdentityUserExistsRoleDto input)
    {
        var res = await CrudService.ExistsRoleAsync(userId,input);
        if (res)
        {
            return Ok();
            
        }
        else
        {
            return NotFound();
        }
    }

}