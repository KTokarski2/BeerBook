public interface IAuthService
{
    Task<string?> Login(LoginRequest request);
    Task Register(RegisterRequest request);
    Task<bool>CheckUserExist(string email);
}