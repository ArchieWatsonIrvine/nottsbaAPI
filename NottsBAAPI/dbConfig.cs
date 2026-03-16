using Microsoft.Data.Sqlite;

namespace NottsBAAPI
{
    public class dbConfig
    {
        public const string ConnectionString = "Data Source=dev.db";

        public static void SetupDatabase()
        {
            try
            {
                using var conn = new SqliteConnection(ConnectionString);
                conn.Open();
                using var cmd = conn.CreateCommand();

                // ==========================================
                // 1. CREATE THE TABLE AND EXECUTE IMMEDIATELY
                // ==========================================
                cmd.CommandText = @"
        CREATE TABLE IF NOT EXISTS users (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Username TEXT NOT NULL UNIQUE,
            Role TEXT NOT NULL,
            RefreshToken TEXT,
            Password TEXT NOT NULL,
            Forename TEXT NOT NULL,
            Surname TEXT NOT NULL
        );
    ";

                // You MUST execute the creation command before trying to insert data!
                cmd.ExecuteNonQuery();

                // ==========================================
                // 2. LOOP THROUGH AND INSERT DATA
                // ==========================================
                var items = DummyData.Users;
                foreach (var item in items)
                {
                    // Notice the column names are hardcoded (Username, Role, etc.)
                    // and the values are pulled from the 'item' variable.
                    cmd.CommandText = $@"
            INSERT INTO users (Username, Role, RefreshToken, Password, Forename, Surname) 
            SELECT '{item.Username}', '{item.Role}', NULL, '{item.Password}', '{item.Forename}', '{item.Surname}' 
            WHERE NOT EXISTS (SELECT 1 FROM users WHERE Username = '{item.Username}');
        ";

                    // Execute the insert for THIS specific user before looping to the next
                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("Database 'dev.db' initialized with the 'users' table.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
            }
        }
    }
}
