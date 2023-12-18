namespace Enter.ENB.Identity.Application.Contracts.Jwt.Dtos;

public class EntJwtResultDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpireAt { get; set; }
    public DateTime ExpireAtUtc { get; set; }
}