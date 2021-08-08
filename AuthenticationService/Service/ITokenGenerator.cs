namespace AuthenticationService.Service
{
    public interface ITokenGenerator
    {
        string GetJWTToken(string userId);
    }
}
