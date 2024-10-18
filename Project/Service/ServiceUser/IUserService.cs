using Project.DTO;

namespace Project.Service;

public interface IUserService
{
    public Task RegisterUser(RegisterModelDTO model);
    public Task<InfoAboutTokenDTO> LoginUser(LoginRequestDTO loginRequest);
}