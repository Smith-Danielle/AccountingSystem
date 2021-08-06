using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace AccountingSystem
{
    public class DapperReportsRepository : IReportsRepository
    {
        private readonly IDbConnection _connection;

        public DapperReportsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Reports> GetAccountActivity(int accountID)
        {
            return _connection.Query<Reports>("SELECT TransactionType,CASE WHEN AccountID_Credit = @accountID THEN 'Credit' WHEN AccountID_Debit = @accountID THEN 'Debit' ELSE NULL END AS Debit_Credit, Amount FROM Checks WHERE AccountID_Credit = @accountID OR AccountID_Debit = @accountID UNION SELECT TransactionType, CASE WHEN AccountID_Credit = @accountID THEN 'Credit' WHEN AccountID_Debit = @accountID THEN 'Debit' ELSE NULL END AS Debit_Credit, Amount FROM Deposits WHERE AccountID_Credit = @accountID OR AccountID_Debit = @accountID UNION SELECT TransactionType,CASE WHEN AccountID_Credit = @accountID THEN 'Credit' WHEN AccountID_Debit = @accountID THEN 'Debit' ELSE NULL END AS Debit_Credit, Amount FROM Invoices WHERE AccountID_Credit = @accountID OR AccountID_Debit = @accountID;",
                new { accountID = accountID });
        }

        public IEnumerable<Reports> GetCondensedActivity(int accountID)
        {
            return _connection.Query<Reports>("SELECT CASE WHEN AccountID_Credit = @accountID THEN 'Credit' WHEN AccountID_Debit = @accountID THEN 'Debit' ELSE NULL END AS Debit_Credit, SUM(Amount) AS Amount FROM Checks WHERE AccountID_Credit = @accountID OR AccountID_Debit = @accountID GROUP BY Debit_Credit UNION SELECT CASE WHEN AccountID_Credit = @accountID THEN 'Credit' WHEN AccountID_Debit = @accountID THEN 'Debit' ELSE NULL END AS Debit_Credit, SUM(Amount) AS Amount FROM Deposits WHERE AccountID_Credit = @accountID OR AccountID_Debit = @accountID GROUP BY Debit_Credit UNION SELECT CASE WHEN AccountID_Credit = @accountID THEN 'Credit' WHEN AccountID_Debit = @accountID THEN 'Debit' ELSE NULL END AS Debit_Credit, SUM(Amount) AS Amount FROM Invoices WHERE AccountID_Credit = @accountID OR AccountID_Debit = @accountID GROUP BY Debit_Credit;",
                new { accountID = accountID });

        }

        public IEnumerable<Reports> GetNetBalance()
        {
            return _connection.Query<Reports>("SELECT CASE WHEN AccountID_Credit = 300 THEN 'Revenue' WHEN AccountID_Debit = 400 OR AccountID_Debit = 401 OR AccountID_Debit = 402 OR AccountID_Debit = 403 THEN 'Expense' ELSE NULL END AS Revenue_Expense, SUM(Amount) AS Amount FROM Deposits WHERE AccountID_Credit = 300 or AccountID_Debit = 400 or AccountID_Debit = 401 or  AccountID_Debit = 402 or AccountID_Debit = 403 GROUP BY Revenue_Expense UNION SELECT CASE WHEN AccountID_Credit = 300 THEN 'Revenue' WHEN AccountID_Debit = 400 OR AccountID_Debit = 401 OR AccountID_Debit = 402 OR AccountID_Debit = 403 THEN 'Expense' ELSE NULL END AS Revenue_Expense, SUM(Amount) AS Amount FROM Invoices WHERE AccountID_Credit = 300 or AccountID_Debit = 400 or AccountID_Debit = 401 or  AccountID_Debit = 402 or AccountID_Debit = 403 GROUP BY Revenue_Expense;");
        }
    }
}
