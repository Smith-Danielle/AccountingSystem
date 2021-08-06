using System;
namespace AccountingSystem
{
    public class Reports
    {
        public Reports()
        {
        }

        //Account Activity
        public string TransactionType { get; set; }
        public int AccountID_Credit { get; set; }
        public int AccountID_Debit { get; set; }
        public string Debit_Credit { get; set; }
        public double Amount { get; set; }
        public string Revenue_Expense { get; set; }




    }
}
