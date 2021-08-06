using System;
using System.Collections.Generic;

namespace AccountingSystem
{
    public interface IInvoiceRepository
    {

        //Enter New Invoice
        public void InsertInvoice(int employeeID, string transactionType, string entryDate, string invoiceNumber, string invoiceDate, string dueDate, string vendorName, double amount, int accountIdDebit);
        //View Invoices
        public IEnumerable<Invoices> GetAllOpenInvoices();
        public IEnumerable<Invoices> GetAllOpenInvoicesSortedDueDate();
        public IEnumerable<Invoices> GetOpenInvoice(int invoiceEntryID);
        public IEnumerable<Invoices> GetOpenDateRange(string startDate, string endDate);
        //Update an Invoice Entry
        public void UpdateInvoiceTransactionType(int invoiceEntryID, string transactionType);
        public void UpdateInvoiceNumber(int invoiceEntryID, string invoiceNumber);
        public void UpdateInvoiceDate(int invoiceEntryID, string invoiceDate);
        public void UpdateDueDate(int invoiceEntryID, string dueDate);
        public void UpdateVendorName(int invoiceEntryID, string vendorName);
        public void UpdateAmount(int invoiceEntryID, double amount);
        public void UpdateAccount(int invoiceEntryID, int account);
        //Delete an Invoice Entry
        public void DeleteInvoice(int invoiceEntryID);
        //For Check Run, need to update invoice status
        public void UpdateSingleInvoiceStatus(string status, int invoiceEntryID);
        public void UpdateInvoiceStatus(string status, string startDate, string endDate);
        
    }
}
