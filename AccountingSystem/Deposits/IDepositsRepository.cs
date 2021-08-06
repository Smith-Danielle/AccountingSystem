using System;
using System.Collections.Generic;

namespace AccountingSystem
{
    public interface IDepositsRepository
    {
        public void InsertDeposit(int employeeID, string depositeDate, double amount);

        public IEnumerable<Deposits> GetAllDeposits();
    }
}
