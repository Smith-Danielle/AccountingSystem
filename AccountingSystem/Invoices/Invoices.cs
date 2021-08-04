using System;
using System.Collections.Generic;

namespace AccountingSystem
{
    public class Invoices
    {
        public Invoices()
        {
        }
        public int InvoiceEntryID { get; set; }
        public int EmployeeID { get; set; }
        public string TransactionType { get; set; }
        public string EntryDate { get; set; } //be sure to display as .Date to ommit the time portion
        public string Status { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; } //be sure to display as .Date to ommit the time portion
        public string DueDate { get; set; } //be sure to display as .Date to ommit the time portion
        public string VendorName { get; set; }
        public double Amount { get; set; }
        public int AccountID_Debit { get; set; }
        public int AccountID_Credit { get; set; }
        public string FirstName { get; set; } //From Employee Table
        public string LastName { get; set; } //From Employee Table
        public string EmployeeName { get; set; } //From Concat Inner Join Employee Table
        public string AccountName { get; set; } //From Account Table
        
    }   
}
