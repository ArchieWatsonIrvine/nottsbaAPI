using NottsBAAPI.DTO.Auth;
using NottsBAAPI.Models.Auth;

namespace NottsBAAPI;

public class DummyData
{
    public static List<UserInfo> Users = new()
    {
         new UserInfo() { Id = 1, Username = "admin", Password = "adminsetup", Role = "Admin" },
         new UserInfo() { Id = 2, Username = "user", Password = "user", Role = "User" }
    };
}