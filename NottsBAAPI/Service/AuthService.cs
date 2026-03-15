using NottsBAAPI.DTO.Auth;
using NottsBAAPI.Interface;
using NottsBAAPI.Models;
using NottsBAAPI.Models.Auth;

namespace NottsBAAPI.Service;

public class AuthService : IAuthNottsBA
{

    async Task<User> IAuthNottsBA.ValidateUser(UserDTO userDTO)
    {
        User? user = DummyData.Users.FirstOrDefault(x => x.Username == userDTO.Username && x.Password == userDTO.Password);

        if (user == null) return null;
        return user;
    }

    Task<BaseResponse> IAuthNottsBA.LogoutUser(UserLogoutDTO userLogoutDTO)
    {
        throw new NotImplementedException();
    }
}
