using Enter.ENB.Ddd.Application.Dtos;

namespace Enter.ENB.Identity.Application.Contracts.Roles.Dtos;

public class EntRoleDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string Title { get; set; }
}