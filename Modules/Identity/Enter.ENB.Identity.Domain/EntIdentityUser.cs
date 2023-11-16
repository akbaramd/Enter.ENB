using Enter.ENB.Domain.Auditing;
using Enter.ENB.Identity.Domain.Shared;
using Enter.ENB.Statics;

namespace Enter.ENB.Identity.Domain;

public  class EntIdentityUser : FullAuditedAggregateRoot<Guid>
{
    private EntIdentityUser()
    {
    }

    public EntIdentityUser(Guid id,string userName)
    {
        Id = id;
        SetUserName(userName);
    }

    public string UserName { get; private set; }
    public string NormalizedUserName { get; private set; }

    public string PhoneNumber { get; private set; }
    public string NormalizedPhoneNumber { get; private set; }
    public string? Password { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public EntUserStatus Status { get; private set; }

    public void Activate()
    {
        Status = EntUserStatus.Active;
    }

    public void Disable()
    {
        Status = EntUserStatus.Disable;
    }

    public void Ban()
    {
        Status = EntUserStatus.Banned;
    }

    public void SetName(string firstName, string lastName)
    {
        EntCheck.NotNullOrWhiteSpace(firstName, nameof(firstName));
        
        FirstName = firstName;
        LastName = lastName;
    }

    public void SetPassword(string password)
    {
        EntCheck.NotNullOrWhiteSpace(password, nameof(password), minLength:6);
        Password = password;
    }

    public void SetPhoneNumber(string phoneNumber)
    {
        EntCheck.NotNullOrWhiteSpace(phoneNumber, nameof(phoneNumber), 11, 10);
        
        PhoneNumber = phoneNumber;
        NormalizedPhoneNumber = phoneNumber.Trim().ToLower()
            .Replace("+", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("-", "");
    }

    public void SetUserName(string userName)
    {
        
        EntCheck.NotNullOrWhiteSpace(userName, nameof(userName), 18, 4);
        
        UserName = userName;
        NormalizedUserName = userName.Trim().ToUpperInvariant();
    }
}