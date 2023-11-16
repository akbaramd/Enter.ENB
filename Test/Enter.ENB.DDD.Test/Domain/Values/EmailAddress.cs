using Enter.ENB.Domain.Values;

namespace Domain.Values;

public class EmailAddress : EntValueObject
{
    public string Email { get; }

    private EmailAddress()
    {
    }

    public EmailAddress(string email)
    {
        Email = email;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        if (Email != null)
        {
            yield return Email;
        }
    }
}
