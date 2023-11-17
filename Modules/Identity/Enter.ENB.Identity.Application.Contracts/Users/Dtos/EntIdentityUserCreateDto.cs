namespace Enter.ENB.Identity.Application.Contracts.Users.Dtos;

public class EntIdentityUserCreateDto 
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}