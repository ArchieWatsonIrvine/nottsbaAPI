using Microsoft.Data.Sqlite;
using NottsBAAPI.DTO.Auth;
using NottsBAAPI.Interface;
using NottsBAAPI.Models;
using NottsBAAPI.Models.Auth;

namespace NottsBAAPI.Service;

public class AuthService : IAuthNottsBA
{
    readonly string _connectionString;

    public AuthService()
    {
        _connectionString = dbConfig.ConnectionString;
    }

    async Task<User> IAuthNottsBA.ValidateUser(UserDTO userDTO)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();

        cmd.CommandText = $"SELECT * FROM users WHERE Username = @username AND Password = @password LIMIT 1;";
        cmd.Parameters.AddWithValue("@username", userDTO.Username.ToLower());
        cmd.Parameters.AddWithValue("@password", userDTO.Password);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new User
            {
                Id = Convert.ToInt32(reader["Id"]),
                Username = reader["Username"].ToString(),
                Role = reader["Role"].ToString(),
                Forename = reader["Forename"].ToString(),
                Surname = reader["Surname"].ToString()
            };
        }

        return null;
    }

    Task<BaseResponse> IAuthNottsBA.LogoutUser(UserLogoutDTO userLogoutDTO)
    {
        throw new NotImplementedException();
    }
}
