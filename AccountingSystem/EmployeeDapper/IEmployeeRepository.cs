using System;
using System.Collections.Generic;

namespace AccountingSystem
{
    public interface IEmployeeRepository
    {
        //Get Employee Password
        public IEnumerable<Employees> GetEmployee(string firstName, string lastName);
        //Insert New Employee
        public void InsertEmployee(string firtName, string lastName, string password);
    }
}
