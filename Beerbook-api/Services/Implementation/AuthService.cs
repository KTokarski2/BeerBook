using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly PgRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthService(PgRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<string?> Login(LoginRequest request)
    {
        var user = await _repository.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return null;
        }
        return GenerateJwtToken(user, user.Role);
    }

    public async Task Register(RegisterRequest request)
    {
        var role = await _repository.Roles.FirstOrDefaultAsync(r => r.Id == 1);

        var user = new User
        {
            Nickname = request.Nickname,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = role
        };

        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
    }

    public async Task<bool> CheckUserExist(string email)
    {
        var user = await _repository.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return false;
        }
        return true;
    }

    private string GenerateJwtToken(User user, Role role)
    {
        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, role.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds
        ); 

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}