using System;
namespace AccountingSystem
{
    public class Checks
    {
        public Checks()
        {
        }
        public int CheckID { get; set; }
        public int EmployeeID { get; set; }
        public string TransactionType { get; set; }
        public DateTime CheckDate { get; set; } //be sure to display as .Date to ommit the time portion
        public int InvoiceEntryID { get; set; }
        public double Amount { get; set; }
        public int AccountID_Debit { get; set; }
        public int AccountID_Credit { get; set; }
    }
}
