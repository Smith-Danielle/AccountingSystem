using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace AccountingSystem
{
    public class DapperChecksRepository : IChecksRepository
    {
        private readonly IDbConnection _connection;

        public DapperChecksRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void InsertSingleCheck(int employeeID, string checkDate, int invoiceEntryID, double amount)
        {
            _connection.Execute("INSERT INTO Checks (EmployeeID, CheckDate, InvoiceEntryID, Amount) VALUES (@employeeID, @checkDate, @invoiceEntryID, @amount);",
                new { employeeID = employeeID, checkDate = checkDate, invoiceEntryID = invoiceEntryID, amount = amount });
        }

        public IEnumerable<Checks> GetSingleCheck(int invoiceEntryID)
        {
            return _connection.Query<Checks>("select c.transactiontype, c.checkID, c.checkdate, c.amount, c.invoiceentryid, a.accountname, concat(e.firstname, ' ', e.lastname) as employeename, i.vendorname, i.duedate, i.invoicenumber, i.invoicedate, i.transactiontype as InvoiceTransactionType from checks as c inner join invoices as i on c.invoiceentryid = i.invoiceentryid inner join employees as e on e.employeeid = c.employeeid inner join accounts as a on a.accountid = i.accountid_Debit where c.invoiceentryID = @invoiceEntryID; ",
                new { invoiceEntryID = invoiceEntryID });
        }

        public void InsertChecks(string startDate, string endDate)
        {
            _connection.Execute("INSERT INTO Checks (InvoiceEntryID, Amount) SELECT InvoiceEntryID, Amount FROM Invoices WHERE Status = 'OPEN' AND DueDate BETWEEN @startdate AND @endDate;",
                new { startDate = startDate, endDate = endDate });
        }

        public IEnumerable<Checks> GetNullEmpIDChecks()
        {
            return _connection.Query<Checks>("SELECT * FROM Checks WHERE EmployeeID IS NULL;");
        }

        public void UpdateChecks(int employeeID, string checkDate, int checkID)
        {
            _connection.Execute("UPDATE Checks SET EmployeeID = @employeeID, CheckDate = @checkDate WHERE CheckID = @checkID;",
                new { employeeID = employeeID, checkDate = checkDate, checkID = checkID });
        }

        public IEnumerable<Checks> GetCheckRun(int startCkID, int endCkID)
        {
            return _connection.Query<Checks>("select c.transactiontype, c.checkID, c.checkdate, c.amount, c.invoiceentryid, a.accountname, concat(e.firstname, ' ', e.lastname) as employeename, i.vendorname, i.duedate, i.invoicenumber, i.invoicedate, i.transactiontype as InvoiceTransactionType from checks as c inner join invoices as i on c.invoiceentryid = i.invoiceentryid inner join employees as e on e.employeeid = c.employeeid inner join accounts as a on a.accountid = i.accountid_Debit where c.checkid between @startCkID and @endCkID;",
                new { startCkID = startCkID, endCkID = endCkID });
        }

        public IEnumerable<Checks> GetAllChecks()
        {
            return _connection.Query<Checks>("SELECT * FROM Checks;");
        }

    }
}
