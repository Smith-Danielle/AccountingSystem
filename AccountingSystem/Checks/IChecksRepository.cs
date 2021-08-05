using System;
using System.Collections.Generic;

namespace AccountingSystem
{
    public interface IChecksRepository
    {
        //Create Checks. Insert Into Checks Table
        public void InsertSingleCheck(int employeeID, string checkDate, int invoiceEntryID, double amount);
        public void InsertChecks(string startDate, string endDate);
        
        public void UpdateChecks(int employeeID, string checkDate, int checkID);
        public IEnumerable<Checks> GetSingleCheck(int invoiceEntryID);
        public IEnumerable<Checks> GetAllChecks();
        
        public IEnumerable<Checks> GetCheckRun(int startCkID, int endCkID);
        public IEnumerable<Checks> GetNullEmpIDChecks();

    }
}
