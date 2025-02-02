using System;
using Npgsql;

public class AccountManager
{
    private readonly string connectionString = "Host=localhost;Username=postgres;Password=password;Database=mydb";

    public void RegisterUser()
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();

        Console.Write("Enter password: ");
        var password = Console.ReadLine();

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var command = new NpgsqlCommand("INSERT INTO users (username, password) VALUES (@username, @password)", connection);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@password", password);

        command.ExecuteNonQuery();
        Console.WriteLine("User registered successfully.");
    }

    public int? Login()
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();

        Console.Write("Enter password: ");
        var password = Console.ReadLine();

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var command = new NpgsqlCommand("SELECT id FROM users WHERE username = @username AND password = @password", connection);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@password", password);

        var userId = command.ExecuteScalar();
        if (userId != null)
        {
            Console.WriteLine("Login successful.");
            return Convert.ToInt32(userId);
        }

        Console.WriteLine("Invalid username or password.");
        return null;
    }
}