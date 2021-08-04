using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace AccountingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeAccess empAccess = new EmployeeAccess();
            int login;
            bool run = true;
            do
            {
                Console.WriteLine("Welcome to AccountingSystems 2021");
                Console.WriteLine("Please select a number below to begin");
                Console.WriteLine("1: Create an Account");
                Console.WriteLine("2: Login");
                Console.WriteLine("3: Exit");

                bool tryLog = int.TryParse(Console.ReadLine(), out login);
                Console.Clear();
                if (login == 1)
                {
                    empAccess.CreateAccount();
                }
                else if (login == 2)
                {
                    empAccess.Login();
                }
                else if (login == 3)
                {
                    run = false;
                }
                
            } while (empAccess.EmpID == 0 && run == true);
            // Need to decide on welcome loop, and if it needs to be there after 1st failure
            // Main Menu access still needs to have empAccess.EmpID value
            // The exit option 3 will be created at the end (App is closed, thanks)
            do
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine("Please select a number below to access the Module");
                Console.WriteLine("1: Invoices");
                Console.WriteLine("2: Checks");
                Console.WriteLine("3: Deposits");
                Console.WriteLine("4: Reports");
                Console.WriteLine("5: Exit");
                int module = int.Parse(Console.ReadLine());
                Console.Clear();

                if (module == 1)
                {
                    int invoiceAction;
                    do
                    {
                        InvoicesModule inv = new InvoicesModule();
                        Console.WriteLine("Invoices");
                        Console.WriteLine("Please select a number below");
                        Console.WriteLine("1: Enter Invoices");
                        Console.WriteLine("2: View Open Invoices");
                        Console.WriteLine("3: Update an Invoice");
                        Console.WriteLine("4: Delete an Invoice");
                        Console.WriteLine("5: Exit back to Main Menu");
                        bool moveforward = int.TryParse(Console.ReadLine(), out invoiceAction);
                        Console.Clear();

                        if (invoiceAction == 1)
                        {
                            inv.InvoiceEntry(empAccess.EmpID);
                        }
                        else if (invoiceAction == 2)
                        {
                            inv.ViewOpenInvoices(empAccess.EmpID);
                        }
                    } while (invoiceAction != 5);


                }
            } while (empAccess.EmpID != 0);

            //Update Invoice Entry
            /*Console.WriteLine("To update an invoice, you will need the Invoice Entry ID.");
            Console.WriteLine("Please select a number below:");
            Console.WriteLine("1: See open invoice list");
            Console.WriteLine("2: Enter Invoice Entry ID");
            int response = int.Parse(Console.ReadLine());
            int invoiceEntryID = 0;
            if (response == 1)
            {
                Console.WriteLine("Would you like to see a list sorted by Invoice Entry ID or Due Date?");
                Console.WriteLine("Please select a number below:");
                Console.WriteLine("1: Sorted by Invoice Entry ID");
                Console.WriteLine("2: Sorted by Due Date");
                int response1 = int.Parse(Console.ReadLine());
                if (response1 == 1)
                {
                    var open = repo1.GetAllOpenInvoices();
                    Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Inv. Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                    foreach (var item in open)
                    {
                        Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                    }
                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                    Console.WriteLine("Please enter the Invoice Entry ID to continue");
                    invoiceEntryID = int.Parse(Console.ReadLine());

                }
                if (response1 == 2)
                {
                    var openSort = repo1.GetAllOpenInvoicesSortedDueDate();
                    Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Inv. Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                    foreach (var item in openSort)
                    {
                        Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                    }
                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                    Console.WriteLine("Please enter the Invoice Entry ID to continue");
                    invoiceEntryID = int.Parse(Console.ReadLine());
                }

            }
            else if (response == 2)
            {
                Console.WriteLine("Please enter the Invoice Entry ID to continue");
                invoiceEntryID = int.Parse(Console.ReadLine());
            }
            var openInvoices = repo1.GetOpenInvoice(invoiceEntryID);
            string status = string.Empty;
            foreach (var item in openInvoices)
            {
                status = item.Status;
            }
            int updateType = 0;
            if (status != "OPEN")
            {
                Console.WriteLine("The Invoice ID you entered in not open.");
            }
            else
            {
                Console.WriteLine("What would you like to update?");
                Console.WriteLine("Please select a number below:");
                Console.WriteLine("1: Transaction Type");
                Console.WriteLine("2: Invoice Number");
                Console.WriteLine("3: Invoice Date");
                Console.WriteLine("4: Due Date");
                Console.WriteLine("5: Vendor Name");
                Console.WriteLine("6: Amount");
                Console.WriteLine("7: Account");
                updateType = int.Parse(Console.ReadLine());
            }
            if (updateType == 1)
            {
                Console.WriteLine("Enter the updated Transaction Type. Invoice or Credit");
                string type = Console.ReadLine();
                repo1.UpdateInvoiceTransactionType(invoiceEntryID, type);
            }
            if (updateType == 2)
            {
                Console.WriteLine("Enter the updated Invoice Number.");
                string invoice = Console.ReadLine();
                repo1.UpdateInvoiceNumber(invoiceEntryID, invoice);
            }
            if (updateType == 3)
            {
                Console.WriteLine("Enter the updated Invoice Date");
                string date = Convert.ToDateTime(Console.ReadLine()).ToString("yyyy-MM-dd");
                repo1.UpdateInvoiceDate(invoiceEntryID, date);
            }
            if (updateType == 4)
            {
                Console.WriteLine("Enter the updated Due Date.");
                string due = Convert.ToDateTime(Console.ReadLine()).ToString("yyyy-MM-dd");
                repo1.UpdateDueDate(invoiceEntryID, due);
            }
            if (updateType == 5)
            {
                Console.WriteLine("Enter the updated Vendor Name");
                string vendor = Console.ReadLine();
                repo1.UpdateVendorName(invoiceEntryID, vendor);
            }
            if (updateType == 6)
            {
                Console.WriteLine("Enter the updated Amount");
                double amount = Convert.ToDouble(Console.ReadLine());
                repo1.UpdateAmount(invoiceEntryID, amount);
            }
            if (updateType == 7)
            { 
                Console.WriteLine("Enter the updated Account Number");
                Console.WriteLine("400: Rent");
                Console.WriteLine("401: Utilites");
                Console.WriteLine("402: Repair & Maintenance");
                Console.WriteLine("403: Office Supplies");
                int account = int.Parse(Console.ReadLine());
                repo1.UpdateAccount(invoiceEntryID, account);
            }*/

            //Delete Invoice
            /*Console.WriteLine("To delete an invoice, you will need the Invoice Entry ID.");
            Console.WriteLine("Please select a number below:");
            Console.WriteLine("1: See open invoice list");
            Console.WriteLine("2: Enter Invoice Entry ID");
            int response = int.Parse(Console.ReadLine());
            int invoiceEntryID = 0;
            if (response == 1)
            {
                var open = repo1.GetAllOpenInvoices();
                Console.WriteLine(String.Format("{0,-6} | {1, -17} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Invoice Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                Console.WriteLine("________________________________________________________________________________________________________________________________________________________________________________________________________");
                foreach (var item in open)
                {
                    Console.WriteLine(String.Format("{0,-6} | {1, -17} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                }
                Console.WriteLine("________________________________________________________________________________________________________________________________________________________________________________________________________");
                Console.WriteLine("Please enter the Invoice Entry ID to continue");
                invoiceEntryID = int.Parse(Console.ReadLine());

            }
            else if (response == 2)
            {
                Console.WriteLine("Please enter the Invoice Entry ID to continue");
                invoiceEntryID = int.Parse(Console.ReadLine());
            }
            var openInvoices = repo1.GetOpenInvoice(invoiceEntryID);
            string status = string.Empty;
            foreach (var item in openInvoices)
            {
                status = item.Status;
            }
            int finalDelete = 0;
            if (status != "OPEN")
            {
                Console.WriteLine("The Invoice ID you entered in not open.");
            }
            else
            {
                Console.WriteLine("Are you sure you want to delete the following invoice?");
                var delete = repo1.GetOpenInvoice(invoiceEntryID);
                Console.WriteLine(String.Format("{0,-6} | {1, -17} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Invoice Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                Console.WriteLine("________________________________________________________________________________________________________________________________________________________________________________________________________");
                foreach (var item in delete)
                {
                    Console.WriteLine(String.Format("{0,-6} | {1, -17} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                }
                Console.WriteLine("________________________________________________________________________________________________________________________________________________________________________________________________________");
                Console.WriteLine("Please select a number below:");
                Console.WriteLine("1: Delete");
                Console.WriteLine("2: Exit");
                finalDelete = int.Parse(Console.ReadLine());
            }
            if (finalDelete == 1)
            {
                repo1.DeleteInvoice(invoiceEntryID);
            }
            if (finalDelete == 2)
            {
                //break;
            }*/

        }
    }
}
