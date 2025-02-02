<<<<<<< HEAD
using System;

public class Transaction
{
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public bool IsIncome { get; set; }

    public Transaction(DateTime date, decimal amount, string description, bool isIncome)
    {
        Date = date;
        Amount = amount;
        Description = description;
        IsIncome = isIncome;
    }

    public override string ToString()
    {
        return $"{Date.ToString("yyyy-MM-dd")}, {Amount}, {Description}, {(IsIncome ? "Income" : "Expense")}";
    }
=======
using System;

public class Transaction
{
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public bool IsIncome { get; set; }

    public Transaction(DateTime date, decimal amount, string description, bool isIncome)
    {
        Date = date;
        Amount = amount;
        Description = description;
        IsIncome = isIncome;
    }

    public override string ToString()
    {
        return $"{Date.ToString("yyyy-MM-dd")}, {Amount}, {Description}, {(IsIncome ? "Income" : "Expense")}";
    }
>>>>>>> e23298f681767ba557aacefc55a4cd5ff8645efe
}