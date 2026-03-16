using Microsoft.Data.Sqlite;

namespace NottsBAAPI;

public class dbConfig
{
    public const string ConnectionString = "Data Source=dev.db";

    public async static void SetupDatabase()
    {
        try
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();

            await CreateTables(cmd);

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
            for (int i = 0; i < DummyData.DummyNews.Count; i++)
            {
                var news = DummyData.DummyNews[i];
                cmd.CommandText = @"
                    INSERT INTO news (CatagoryId, Date, Title, Content, Link) 
                    VALUES (@catId, @date, @title, @content, @link);";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@catId", news.CatagoryId);
                cmd.Parameters.AddWithValue("@date", news.Date);
                cmd.Parameters.AddWithValue("@title", news.Title);
                cmd.Parameters.AddWithValue("@content", news.Content);
                cmd.Parameters.AddWithValue("@link", news.Link);

                cmd.ExecuteNonQuery();

            }

            Console.WriteLine("Database 'dev.db' initialized with the 'users' table.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database Error: {ex.Message}");
        }
    }


    public async static Task CreateTables(SqliteCommand cmd)
    {
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

        CREATE TABLE IF NOT EXISTS news (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            CatagoryId INTEGER NOT NULL,
            Date TEXT NOT NULL,
            Content TEXT NOT NULL,
            Title TEXT NOT NULL,
            Link TEXT,    -- Internal Angular route
            Href TEXT     -- External PDF link
        );

        CREATE TABLE IF NOT EXISTS clubs (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            Location TEXT,
            ContactEmail TEXT
        );
    ";
        cmd.ExecuteNonQuery();

    }
}
