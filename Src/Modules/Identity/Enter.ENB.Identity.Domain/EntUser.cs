using Enter.ENB.Domain.Auditing;
using Enter.ENB.Identity.Domain.Shared;

namespace Enter.ENB.Identity.Domain;

public class EntUser : FullAuditedAggregateRoot<Guid>
{
    
    protected EntUser () {}

    public EntUser(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; private set; }
    public string? Password { get; private set; }
    public string? FirstName { get; private set; } 
    public string? LastName { get; private set; }
    public EntUserStatus Status { get; private set; }

    public void Activate() => Status = EntUserStatus.Active;
    public void Disable() => Status = EntUserStatus.Disable;
    public void Ban() => Status = EntUserStatus.Banned;

    public void SetName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    public void SetPassword(string password)
    {
        Password = password;
    }
}