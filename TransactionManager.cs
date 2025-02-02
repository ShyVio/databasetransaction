using System;
using Npgsql;

public class TransactionManager
{
    private readonly string connectionString = "Host=localhost;Username=postgres;Password=password;Database=mydb";

    public void AddTransaction(int userId)
    {
        Console.Write("Enter amount: ");
        var amount = decimal.Parse(Console.ReadLine()!);

        Console.Write("Enter description: ");
        var description = Console.ReadLine();

        Console.Write("Is this income? (yes/no): ");
        var isIncome = Console.ReadLine()!.ToLower() == "yes";

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var command = new NpgsqlCommand("INSERT INTO transactions (user_id, amount, description, is_income, date) VALUES (@user_id, @amount, @description, @is_income, @date)", connection);
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@is_income", isIncome);
        command.Parameters.AddWithValue("@date", DateTime.Now);

        command.ExecuteNonQuery();
        Console.WriteLine("Transaction added.");
    }

    public void RemoveTransaction(int userId)
    {
        Console.Write("Enter transaction ID to remove: ");
        var transactionId = int.Parse(Console.ReadLine()!);

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var command = new NpgsqlCommand("DELETE FROM transactions WHERE id = @id AND user_id = @user_id", connection);
        command.Parameters.AddWithValue("@id", transactionId);
        command.Parameters.AddWithValue("@user_id", userId);

        var rowsAffected = command.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine("Transaction removed.");
        }
        else
        {
            Console.WriteLine("Transaction not found or not owned by you.");
        }
    }

    public void ViewBalance(int userId)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var command = new NpgsqlCommand("SELECT SUM(CASE WHEN is_income THEN amount ELSE -amount END) FROM transactions WHERE user_id = @user_id", connection);
        command.Parameters.AddWithValue("@user_id", userId);

        var balance = command.ExecuteScalar();
        Console.WriteLine($"Current Balance: {(balance == DBNull.Value ? 0 : Convert.ToDecimal(balance)):C}");
    }

    public void ViewTransactions(int userId)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var command = new NpgsqlCommand("SELECT id, date, amount, description, is_income FROM transactions WHERE user_id = @user_id ORDER BY date", connection);
        command.Parameters.AddWithValue("@user_id", userId);

        using var reader = command.ExecuteReader();

        Console.WriteLine("Transactions:");
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var date = reader.GetDateTime(1);
            var amount = reader.GetDecimal(2);
            var description = reader.GetString(3);
            var isIncome = reader.GetBoolean(4);

            Console.WriteLine($"{id}: {date:yyyy-MM-dd}, {amount:C}, {description}, {(isIncome ? "Income" : "Expense")}");
        }
    }
}
