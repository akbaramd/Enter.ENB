using Enter.ENB.Ddd.Application.Dtos;

namespace Enter.ENB.Identity.Application.Contracts.Users.Dtos;

public class UserDto : FullAuditedEntityDto<Guid>
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}