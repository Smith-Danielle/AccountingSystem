using System;
namespace AccountingSystem
{
    public interface IInvoiceRepository
    {

        //Enter New Invoice
        public void InsertInvoice(int employeeID, string transactionType, string entryDate, string invoiceNumber, string invoiceDate, string dueDate, string vendorName, double amount, int accountIdDebit);
        //View All Open Invoices

        //Update an Invoice Entry
        //Delete an Invoice Entry
    }
}
