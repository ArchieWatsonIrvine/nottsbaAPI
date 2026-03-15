using System.Diagnostics.Contracts;

namespace NottsBAAPI.Models.Auth;

public class User : BaseResponse
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}