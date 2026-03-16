using NottsBAAPI.DTO.Auth;
using NottsBAAPI.Models.Auth;

namespace NottsBAAPI;

public class DummyData
{
    public static List<UserInfo> Users = new()
    {
         new UserInfo() { Id = 1, Username = "admin", Password = "adminsetup", Role = "Admin", Forename = "admin", Surname = "admin", RefreshToken = "asdqwerasdewwqr" },
         new UserInfo() { Id = 2, Username = "user", Password = "userpassword", Role = "User", Forename = "userforname", Surname = "usersurname", RefreshToken = "asdqwerasdewwqr" }
    };
}