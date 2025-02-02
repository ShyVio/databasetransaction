using System;

class Program
{
    static void Main(string[] args)
    {
        var accountManager = new AccountManager();
        var transactionManager = new TransactionManager();

        while (true)
        {
            Console.WriteLine("Personal Finance App");
            Console.WriteLine("1. Register User");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    accountManager.RegisterUser();
                    break;
                case "2":
                    var userId = accountManager.Login();
                    if (userId != null)
                    {
                        RunUserSession(userId.Value, transactionManager);
                    }
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void RunUserSession(int userId, TransactionManager transactionManager)
    {
        while (true)
        {
            Console.WriteLine("User Menu");
            Console.WriteLine("1. Add Transaction");
            Console.WriteLine("2. Remove Transaction");
            Console.WriteLine("3. View Balance");
            Console.WriteLine("4. View Transactions");
            Console.WriteLine("5. Logout");
            Console.Write("Select an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    transactionManager.AddTransaction(userId);
                    break;
                case "2":
                    transactionManager.RemoveTransaction(userId);
                    break;
                case "3":
                    transactionManager.ViewBalance(userId);
                    break;
                case "4":
                    transactionManager.ViewTransactions(userId);
                    break;
                case "5":
                    Console.WriteLine("Logged out.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}