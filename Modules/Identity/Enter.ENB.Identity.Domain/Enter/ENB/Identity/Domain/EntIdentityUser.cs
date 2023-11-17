using Enter.ENB.Domain.Auditing;
using Enter.ENB.Exceptions;
using Enter.ENB.Identity.Domain.Shared;
using Enter.ENB.Statics;

namespace Enter.ENB.Identity.Domain;

public class EntIdentityUser : FullAuditedAggregateRoot<Guid>
{
    private EntIdentityUser()
    {
        Roles = new List<EntIdentityRole>();
    }

    public EntIdentityUser(Guid id, string userName)
    {
        Id = id;
        SetUserName(userName);
        Roles = new List<EntIdentityRole>();
    }

    public string UserName { get; private set; }
    public string NormalizedUserName { get; private set; }

    public string? PhoneNumber { get; private set; }
    public string? NormalizedPhoneNumber { get; private set; }

    public string? Email { get; private set; }
    public string? NormalizedEmail { get; private set; }
    public string? Password { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }

    public EntUserStatus Status { get; private set; }

    public virtual ICollection<EntIdentityRole> Roles { get; }

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
        EntCheck.NotNullOrWhiteSpace(password, nameof(password), minLength: 6);
        Password = password;
    }

    public bool CheckRole(EntIdentityRole role)
    {
        return Roles.Any(x => x.Name.Equals(role.Name));
    }
    
    public void AssignRole(EntIdentityRole role)
    {
        if (CheckRole(role))
            throw new EntException("this role already assigned to user");

        Roles.Add(role);
    }

    public void UnAssignRole(EntIdentityRole role)
    {
        if (CheckRole(role))
            Roles.Remove(role);
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

    public void SetEmail(string email)
    {
        EntCheck.NotNullOrWhiteSpace(email, nameof(email), minLength: 6);

        Email = email;
        NormalizedEmail = email.Trim().ToLower()
            .Replace("+", "")
            .Replace("(", "")
            .Replace(")", "");
    }

    public void SetUserName(string userName)
    {
        EntCheck.NotNullOrWhiteSpace(userName, nameof(userName), 18, 4);
        UserName = userName;
        NormalizedUserName = userName.Trim().ToUpperInvariant();
    }

    public bool ComparePassword(string rawPassword)
    {
        return rawPassword == Password;
    }
}