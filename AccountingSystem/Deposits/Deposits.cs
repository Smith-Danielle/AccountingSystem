using System;
namespace AccountingSystem
{
    public class Deposits
    {
        public Deposits()
        {
        }
        public int DepositID { get; set; }
        public int EmployeeID { get; set; }
        public string TransactionType { get; set; }
        public string DepositDate { get; set; } //be sure to display as .Date to ommit the time portion
        public double Amount { get; set; }
        public int AccountID_Debit { get; set; }
        public int AccountID_Credit { get; set; }
        public string FirstName { get; set; } //From Employee Table
        public string LastName { get; set; } //From Employee Table
        public string EmployeeName { get; set; } //From Concat Inner Join Employee Table
    }
}
