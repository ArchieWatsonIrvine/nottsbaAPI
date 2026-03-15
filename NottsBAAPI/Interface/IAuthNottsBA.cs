using NottsBAAPI.DTO.Auth;
using NottsBAAPI.Models;
using NottsBAAPI.Models.Auth;

namespace NottsBAAPI.Interface;

public interface IAuthNottsBA
{
    public Task<User> ValidateUser(UserDTO userDTO);
    public Task<BaseResponse> LogoutUser(UserLogoutDTO userLogoutDTO);
}
