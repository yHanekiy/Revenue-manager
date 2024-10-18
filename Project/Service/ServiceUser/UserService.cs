using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Project.DTO;
using Project.Error;
using Project.Helper;
using Project.Model;
using Project.Repository;

namespace Project.Service;

public class UserService(IUserRepository userRepository, IConfiguration configuration): IUserService
{
    private IConfiguration _configuration = configuration;
    public async Task RegisterUser(RegisterModelDTO model)
    {
        Tuple<string, string> hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);

        User user = new User()
        {
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1),
            RoleType = model.RoleType
        };

        await userRepository.RegisterUser(user);
    }

    public async Task<InfoAboutTokenDTO> LoginUser(LoginRequestDTO loginRequest)
    {
        User? user = await userRepository.GetUserByLogin(loginRequest.Login);
        if (user == null)
        {
            throw new UserIsNotFound();
        }

        string passwordHashFromDb = user.Password;
        
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);

        
        if (passwordHashFromDb != curHashedPassword)
        {
            throw new Unauthorized();
        }

        Claim[] userClaim = new[]
        {
            new Claim(ClaimTypes.Name, loginRequest.Login),
            new Claim(ClaimTypes.Role, user.RoleType.ToString()),
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "http://localhost:5111",
            audience: "http://localhost:5111",
            claims: userClaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );
        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        
        return new InfoAboutTokenDTO(){
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = user.RefreshToken};
    }
}