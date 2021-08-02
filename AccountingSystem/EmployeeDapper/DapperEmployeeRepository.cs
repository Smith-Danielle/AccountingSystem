using System;
using System.Data;
using Dapper;
using System.Collections.Generic;

namespace AccountingSystem
{
    public class DapperEmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _connection;

        public DapperEmployeeRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Employees> GetEmployee(string firstName, string lastName)
        {
            return _connection.Query<Employees>("SELECT * FROM Employees WHERE FirstName = @firstName AND LastName = @lastName;",
                new { firstName = firstName, lastName = lastName });
        }

        public void InsertEmployee(string firstName, string lastName, string password)
        {
            _connection.Execute("INSERT INTO Employees (FirstName, LastName, Password) VALUES (@firstName, @lastName, @password);",
                new { firstName = firstName, lastName = lastName, password = password });
        }
    }
}
