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
                new { employeeID = employeeID, transactionType = transactionType, entryDate = entryDate, invoiceNumber = invoiceNumber, invoiceDate = invoiceDate, dueDate = dueDate, vendorName = vendorName, amount = amount, accountIdDebit = accountIdDebit});
        }
    }
}
