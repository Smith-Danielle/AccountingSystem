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
        public string CheckDate { get; set; } //be sure to display as .Date to ommit the time portion
        public int InvoiceEntryID { get; set; }
        public double Amount { get; set; }
        public int AccountID_Debit { get; set; }
        public int AccountID_Credit { get; set; }
        public string FirstName { get; set; } //From Employee Table
        public string LastName { get; set; } //From Employee Table
        public string EmployeeName { get; set; } //From Concat Inner Join Employee Table
        public string AccountName { get; set; } //From Account Table
        public string VendorName { get; set; } //From Invoice Table
        public string DueDate { get; set; } //From Invoice Table
        public string InvoiceNumber { get; set; } //From Invoice Table
        public string InvoiceDate { get; set; } //From Invoice Table
        public string InvoiceTransactionType { get; set; } //From Invoice Table
    }
}
