using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace AccountingSystem
{
    public class DapperDepositsRepository : IDepositsRepository
    {
        private readonly IDbConnection _connection;

        public DapperDepositsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Deposits> GetAllDeposits()
        {
            return _connection.Query<Deposits>("SELECT d.*, CONCAT(e.FirstName, ' ', e.LastName) as EmployeeName FROM Deposits AS d INNER JOIN Employees AS e ON d.EmployeeID = e.EmployeeID ORDER BY DepositID;");
        }

        public void InsertDeposit(int employeeID, string depositDate, double amount)
        {
            _connection.Execute("INSERT INTO Deposits (EmployeeID, DepositDate, Amount) VALUES (@employeeID, @depositDate, @amount);",
                new { employeeID = employeeID, depositDate = depositDate, amount = amount });

        }
    }
}
