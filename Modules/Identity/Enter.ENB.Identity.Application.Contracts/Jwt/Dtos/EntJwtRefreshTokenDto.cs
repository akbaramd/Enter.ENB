namespace Enter.ENB.Identity.Application.Contracts.Roles.Dtos;

public class EntJwtRefreshTokenDto
{ 
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}