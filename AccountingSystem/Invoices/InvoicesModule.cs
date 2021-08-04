using System;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AccountingSystem
{
    public class InvoicesModule
    {
        public InvoicesModule()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            RepoInvoice = new DapperInvoiceRepository(conn);
        }
        public DapperInvoiceRepository RepoInvoice { get; set; }

        public void InvoiceEntry(int employeeID)
        {

            int nextAction;
            do
            {
                string transactionType = string.Empty;

                int choice;
                do
                {
                    Console.WriteLine("Invoice or Credit Memo");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("1: Invoice");
                    Console.WriteLine("2: Credit");
                    bool question = int.TryParse(Console.ReadLine(), out choice);
                    Console.Clear();
                } while (choice != 1 && choice != 2);
                if (choice == 1)
                {
                    transactionType = "Invoice";
                }
                else if (choice == 2)
                {
                    transactionType = "Credit";
                }

                Console.WriteLine("Enter: Invoice Number");
                string invoiceNumber = Console.ReadLine();
                Console.Clear();

                DateTime invDate;
                bool isDate;
                do
                {
                    Console.WriteLine("Enter: Invoice Date");
                    Console.WriteLine("Please use format MM-DD-YYYY or YYYY-MM-DD");
                    isDate = DateTime.TryParse(Console.ReadLine(), out invDate);
                    Console.Clear();
                } while (isDate == false);
                string invoiceDate = invDate.ToString("yyyy-MM-dd");

                DateTime dDate;
                bool isDue;
                do
                {
                    Console.WriteLine("Enter: Due Date");
                    Console.WriteLine("Please use format MM-DD-YYYY or YYYY-MM-DD");
                    isDue = DateTime.TryParse(Console.ReadLine(), out dDate);
                    Console.Clear();
                } while (isDue == false);
                string dueDate = dDate.ToString("yyyy-MM-dd");

                Console.WriteLine("Enter: Vendor Name");
                string vendorName = Console.ReadLine();
                Console.Clear();

                bool num;
                double amount;
                do
                {
                    Console.WriteLine("Enter: Amount");
                    num = double.TryParse(Console.ReadLine(), out amount);
                    Console.Clear();
                } while (num == false);

                int accountIdDebit;
                do
                {
                    Console.WriteLine("Enter: Account Number");
                    Console.WriteLine("400: Rent");
                    Console.WriteLine("401: Utilites");
                    Console.WriteLine("402: Repair & Maintenance");
                    Console.WriteLine("403: Office Supplies");
                    bool accNum = int.TryParse(Console.ReadLine(), out accountIdDebit);
                    Console.Clear();
                } while (accountIdDebit != 400 && accountIdDebit != 401 && accountIdDebit != 402 && accountIdDebit != 403);

                RepoInvoice.InsertInvoice(employeeID, transactionType, DateTime.Now.ToString("yyyy-MM-dd"), invoiceNumber, invoiceDate, dueDate, vendorName, amount, accountIdDebit);

                do
                {
                    Console.WriteLine("Would you like to enter another invoice?");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("1: Enter additional invoice");
                    Console.WriteLine("2: Exit back to Invoices Menu");
                    bool action = int.TryParse(Console.ReadLine(), out nextAction);
                    Console.Clear();
                } while (nextAction != 1 && nextAction != 2);

            } while (nextAction == 1);
        }
        public void ViewOpenInvoices(int employeeID)
        {
            int nextAction;
            do
            {
                int order;
                do
                {
                    Console.WriteLine("Would you like to view open invoies sorted by Invoice Entry ID or Due Date");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("1: Order by Invoice Entry ID");
                    Console.WriteLine("2: Order by Due Date");
                    bool orderType = int.TryParse(Console.ReadLine(), out order);
                    Console.Clear();
                } while (order != 1 && order != 2);

                if (order == 1)
                {
                    var openInvoices = RepoInvoice.GetAllOpenInvoices();
                    Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Inv. Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                    foreach (var item in openInvoices)
                    {
                        Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                    }
                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");

                }

                if (order == 2)
                {
                    var openInvoices1 = RepoInvoice.GetAllOpenInvoicesSortedDueDate();
                    Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Inv. Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                    foreach (var item in openInvoices1)
                    {
                        Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                    }
                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");

                }

                do
                {
                    Console.WriteLine("Would you like view open invoices in a different order?");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("1: View open invoice in a different order");
                    Console.WriteLine("2: Exit back to Invoices Menu");
                    bool action = int.TryParse(Console.ReadLine(), out nextAction);
                    Console.Clear();
                } while (nextAction != 1 && nextAction != 2);

            } while (nextAction == 1);
        }
    }
}
