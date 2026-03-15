using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NottsBAAPI.DTO.Auth;
using NottsBAAPI.Interface;
using NottsBAAPI.Models;
using NottsBAAPI.Models.Auth;

namespace NottsBAAPI.Controllers;

public class AuthController(IAuthNottsBA authNottsBA) : BaseController
{
    //Login
    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(UserDTO userDTO)
    {
        var user = await authNottsBA.ValidateUser(userDTO);
        if (user.StatusCode != 0) return Unauthorized();

        return Ok(user);
    }

    //Logout

    [HttpPost("logout")]
    public async Task<ActionResult<BaseResponse>> Logout(UserLogoutDTO userLogoutDTO)
    {
        // Implement logout logic here (e.g., invalidate tokens, clear cookies, etc.)
        BaseResponse rsp = await authNottsBA.LogoutUser(userLogoutDTO);

        if (rsp.StatusCode != 0) return Forbid();
        return Ok(rsp);
    }

    //Register
    [HttpPost("register")]
    public async Task<ActionResult<BaseResponse>> Register(UserDTO userDTO)
    {
        // Implement registration logic here (e.g., create a new user, hash passwords, etc.)
        return Ok(new BaseResponse { StatusCode = 0, Message = "Registration successful" });
    }

    //Refresh Access Token
    [HttpPost("refresh-token")]
    public async Task<ActionResult<RefreshReturn>> RefreshToken(string RefreshToken)
    {
        // Implement token refresh logic here (e.g., validate refresh token, issue new access token, etc.)
        return Ok(new RefreshReturn { RefreshToken = "", AccessToken = "" });
    }
}
