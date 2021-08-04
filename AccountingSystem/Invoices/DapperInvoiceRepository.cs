using System;
using System.Data;
using Dapper;
using System.Collections.Generic;

namespace AccountingSystem
{
    public class DapperInvoiceRepository : IInvoiceRepository
    {
        private readonly IDbConnection _connection;

        public DapperInvoiceRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void InsertInvoice(int employeeID, string transactionType, string entryDate, string invoiceNumber, string invoiceDate, string dueDate, string vendorName, double amount, int accountIdDebit)
        {
            _connection.Execute("INSERT INTO Invoices (EmployeeID, TransactionType, EntryDate, InvoiceNumber, InvoiceDate, DueDate, VendorName, Amount, AccountID_Debit) VALUES (@employeeID, @transactionType, @entryDate, @invoiceNumber, @invoiceDate, @dueDate, @vendorName, @amount, @accountIdDebit);",
                new { employeeID = employeeID, transactionType = transactionType, entryDate = entryDate, invoiceNumber = invoiceNumber, invoiceDate = invoiceDate, dueDate = dueDate, vendorName = vendorName, amount = amount, accountIdDebit = accountIdDebit });

            string accountName = string.Empty;
            if (accountIdDebit == 400)
            {
                accountName = "Rent";
            }
            if (accountIdDebit == 401)
            {
                accountName = "Utilities";
            }
            if (accountIdDebit == 402)
            {
                accountName = "Repair & Maintenance";
            }
            if (accountIdDebit == 403)
            {
                accountName = "Office Supplies";
            }
            Console.Clear();
            Console.WriteLine("The following invoice has been entered:");
            Console.WriteLine("_____________________________________________________________________________________________________________________________________");
            Console.WriteLine(String.Format("{0, -17} | {1, -15} | {2, -13} | {3, -13} | {4, -24} | {5, -10} | {6, -22}", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account"));
            Console.WriteLine("_____________________________________________________________________________________________________________________________________");
            Console.WriteLine(String.Format("{0, -17} | {1, -15} | {2, -13} | {3, -13} | {4, -24} | {5, -10} | {6, -22}", transactionType, invoiceNumber, invoiceDate, dueDate, vendorName, amount, accountName));
            Console.WriteLine("_____________________________________________________________________________________________________________________________________");
        }

        public IEnumerable<Invoices> GetAllOpenInvoices()
        {
            return _connection.Query<Invoices>("SELECT i.*, a.AccountName, CONCAT(E.FirstName, ' ', E.Lastname) AS EmployeeName FROM Invoices as i INNER JOIN Accounts AS a ON i.AccountID_Debit = a.AccountID INNER JOIN Employees AS e ON i.EmployeeID = e.EmployeeID WHERE Status = 'OPEN' ORDER BY InvoiceEntryID;");
        }

        public IEnumerable<Invoices> GetAllOpenInvoicesSortedDueDate()
        {
            return _connection.Query<Invoices>("SELECT i.*, a.AccountName, CONCAT(E.FirstName, ' ', E.Lastname) AS EmployeeName FROM Invoices as i INNER JOIN Accounts AS a ON i.AccountID_Debit = a.AccountID INNER JOIN Employees AS e ON i.EmployeeID = e.EmployeeID WHERE Status = 'OPEN' ORDER BY DueDate;");
        }

        public IEnumerable<Invoices> GetOpenInvoice(int invoiceEntryID)
        {
            return _connection.Query<Invoices>("SELECT i.*, a.AccountName, CONCAT(E.FirstName, ' ', E.Lastname) AS EmployeeName FROM Invoices as i INNER JOIN Accounts AS a ON i.AccountID_Debit = a.AccountID INNER JOIN Employees AS e ON i.EmployeeID = e.EmployeeID WHERE Status = 'OPEN' AND InvoiceEntryID = @invoiceEntryID;",
                new { invoiceEntryID = invoiceEntryID });
        }

        public void UpdateInvoiceTransactionType(int invoiceEntryID, string transactionType)
        {
            _connection.Execute("UPDATE Invoices SET TransactionType = @transactionType WHERE InvoiceEntryID = @invoiceEntryID;",
                new { invoiceEntryID = invoiceEntryID, transactionType = transactionType });

            Console.Clear();
            Console.WriteLine($"Invoice Entry ID {invoiceEntryID} has been updated to Transaction Type: {transactionType}");

        }
        public void UpdateInvoiceNumber(int invoiceEntryID, string invoiceNumber)
        {
            _connection.Execute("UPDATE Invoices SET InvoiceNumber = @invoiceNumber WHERE InvoiceEntryID = @invoiceEntryID;",
                new { invoiceEntryID = invoiceEntryID, invoiceNumber = invoiceNumber });

            Console.Clear();
            Console.WriteLine($"Invoice Entry ID {invoiceEntryID} has been updated to Invoice Number: {invoiceNumber}");
        }
        public void UpdateInvoiceDate(int invoiceEntryID, string invoiceDate)
        {
            _connection.Execute("UPDATE Invoices SET InvoiceDate = @invoiceDate WHERE InvoiceEntryID = @invoiceEntryID;",
                new { invoiceEntryID = invoiceEntryID, invoiceDate = invoiceDate });

            Console.Clear();
            Console.WriteLine($"Invoice Entry ID {invoiceEntryID} has been updated to Invoice Date: {invoiceDate}");
        }
        public void UpdateDueDate(int invoiceEntryID, string dueDate)
        {
            _connection.Execute("UPDATE Invoices SET DueDate = @dueDate WHERE InvoiceEntryID = @invoiceEntryID;",
                new { invoiceEntryID = invoiceEntryID, dueDate = dueDate });

            Console.Clear();
            Console.WriteLine($"Invoice Entry ID {invoiceEntryID} has been updated to Due Date: {dueDate}");
        }
        public void UpdateVendorName(int invoiceEntryID, string vendorName)
        {
            _connection.Execute("UPDATE Invoices SET VendorName = @vendorName WHERE InvoiceEntryID = @invoiceEntryID;",
                new { invoiceEntryID = invoiceEntryID, vendorName = vendorName });

            Console.Clear();
            Console.WriteLine($"Invoice Entry ID {invoiceEntryID} has been updated to Vendor Name: {vendorName}");
        }
        public void UpdateAmount(int invoiceEntryID, double amount)
        {
            _connection.Execute("UPDATE Invoices SET Amount = @amount WHERE InvoiceEntryID = @invoiceEntryID;",
                new { invoiceEntryID = invoiceEntryID, amount = amount });

            Console.Clear();
            Console.WriteLine($"Invoice Entry ID {invoiceEntryID} has been updated to Amount: {amount}");
        }
        public void UpdateAccount(int invoiceEntryID, int account)
        {
            _connection.Execute("UPDATE Invoices SET AccountID_Debit = @account WHERE InvoiceEntryID = @invoiceEntryID;",
                new { invoiceEntryID = invoiceEntryID, account = account });

            string accountName = string.Empty;
            if (account == 400)
            {
                accountName = "Rent";
            }
            if (account == 401)
            {
                accountName = "Utilities";
            }
            if (account == 402)
            {
                accountName = "Repair & Maintenance";
            }
            if (account == 403)
            {
                accountName = "Office Supplies";
            }

            Console.Clear();
            Console.WriteLine($"Invoice Entry ID {invoiceEntryID} has been updated to Account: {accountName}");
        }

        public void DeleteInvoice(int invoiceEntryID)
        {
            _connection.Execute("DELETE FROM Invoices WHERE InvoiceEntryID = @invoiceEntryID;",
                new { invoiceEntryID = invoiceEntryID });

            Console.Clear();
            Console.WriteLine($"Invoice Entry ID {invoiceEntryID} has been deleted");
        }
    }
}
