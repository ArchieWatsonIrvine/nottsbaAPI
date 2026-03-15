using NottsBAAPI.DTO.Auth;
using NottsBAAPI.Models.Auth;

namespace NottsBAAPI;

public class DummyData
{
    public static List<User> Users = new List<User>()
    {
         new User() { Id = 1, Username = "admin", Password = "admin", Role = "Admin" },
         new User() { Id = 2, Username = "user", Password = "user", Role = "User" }
    };
}