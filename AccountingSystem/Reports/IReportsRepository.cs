using System;
using System.Collections.Generic;

namespace AccountingSystem
{
    public interface IReportsRepository
    {
        public IEnumerable<Reports> GetAccountActivity(int accountID);

        public IEnumerable<Reports> GetCondensedActivity(int accountID);

        public IEnumerable<Reports> GetNetBalance();
    }
}
