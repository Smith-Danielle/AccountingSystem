using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AccountingSystem
{
    public class ChecksModule
    {
        public ChecksModule()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            RepoChecks = new DapperChecksRepository(conn);
            RepoInvoices = new DapperInvoiceRepository(conn);
        }
        public DapperChecksRepository RepoChecks { get; set; }

        public DapperInvoiceRepository RepoInvoices { get; set; }

        public void CheckRun(int employeeID)
        {

            bool redo = true;
            do
            {

                int checkWrite = 0;
                do
                {
                    Console.WriteLine("__________________________________________________");
                    Console.WriteLine("Would you like to print a single check or a batch?");
                    Console.WriteLine("__________________________________________________");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("1: Single");
                    Console.WriteLine("2: Batch");
                    bool checkType = int.TryParse(Console.ReadLine(), out checkWrite);
                    Console.Clear();
                } while (checkWrite != 1 && checkWrite != 2);

                bool redoSection = false;
                do
                {
                    redoSection = false;

                    if (checkWrite == 1)
                    {
                        int response = 0;
                        do
                        {
                            Console.WriteLine("____________________________________________________________");
                            Console.WriteLine("To print a single check, you will need the Invoice Entry ID.");
                            Console.WriteLine("____________________________________________________________");
                            Console.WriteLine("Please select a number below:");
                            Console.WriteLine("_____________________________");
                            Console.WriteLine("1: See open invoice list");
                            Console.WriteLine("2: Enter Invoice Entry ID");
                            bool invID = int.TryParse(Console.ReadLine(), out response);
                            Console.Clear();
                        } while (response != 1 && response != 2);

                        int invoiceEntryID = 0;

                        if (response == 1)
                        {
                            int response1;
                            bool order;
                            do
                            {
                                Console.WriteLine("____________________________________________________________________");
                                Console.WriteLine("Would you like to see a list sorted by Invoice Entry ID or Due Date?");
                                Console.WriteLine("____________________________________________________________________");
                                Console.WriteLine("Please select a number below:");
                                Console.WriteLine("_____________________________");
                                Console.WriteLine("1: Sorted by Invoice Entry ID");
                                Console.WriteLine("2: Sorted by Due Date");
                                order = int.TryParse(Console.ReadLine(), out response1);
                                Console.Clear();
                            } while (response1 != 1 && response1 != 2);


                            if (response1 == 1)
                            {
                                bool orderID;
                                string status = string.Empty;
                                do
                                {
                                    var open = RepoInvoices.GetAllOpenInvoices();
                                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                                    Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Inv. Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                                    foreach (var item in open)
                                    {
                                        Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                                    }
                                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                                    Console.WriteLine("Please enter the Invoice Entry ID to continue");
                                    orderID = int.TryParse(Console.ReadLine(), out invoiceEntryID);
                                    Console.Clear();

                                    var openInvoices = RepoInvoices.GetOpenInvoice(invoiceEntryID);

                                    foreach (var item in openInvoices)
                                    {
                                        status = item.Status;
                                    }
                                    if (status != "OPEN")
                                    {
                                        Console.WriteLine("The Invoice ID you entered in not open.");
                                    }
                                } while (status != "OPEN");

                            }
                            if (response1 == 2)
                            {
                                bool orderDue;
                                string status1 = string.Empty;
                                do
                                {
                                    var openSort = RepoInvoices.GetAllOpenInvoicesSortedDueDate();
                                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                                    Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Inv. Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                                    foreach (var item in openSort)
                                    {
                                        Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                                    }
                                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                                    Console.WriteLine("Please enter the Invoice Entry ID to continue");
                                    orderDue = int.TryParse(Console.ReadLine(), out invoiceEntryID);
                                    Console.Clear();

                                    var openInvoices1 = RepoInvoices.GetOpenInvoice(invoiceEntryID);

                                    foreach (var item in openInvoices1)
                                    {
                                        status1 = item.Status;
                                    }
                                    if (status1 != "OPEN")
                                    {
                                        Console.WriteLine("The Invoice ID you entered in not open.");
                                    }
                                } while (status1 != "OPEN");

                            }

                        }
                        else if (response == 2)
                        {
                            bool idEntry;
                            string status2 = string.Empty;
                            do
                            {
                                Console.WriteLine("Please enter the Invoice Entry ID to continue");
                                idEntry = int.TryParse(Console.ReadLine(), out invoiceEntryID);
                                Console.Clear();
                                var openInvoices1 = RepoInvoices.GetOpenInvoice(invoiceEntryID);

                                foreach (var item in openInvoices1)
                                {
                                    status2 = item.Status;
                                }
                                if (status2 != "OPEN")
                                {
                                    Console.WriteLine("The Invoice ID you entered in not open.");
                                }
                            } while (status2 != "OPEN");

                        }

                        int finalPrint = 0;
                        do
                        {
                            Console.WriteLine("Are you sure you want to print the following invoice?");
                            var printSingle = RepoInvoices.GetOpenInvoice(invoiceEntryID);
                            Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                            Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Inv. Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                            Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                            foreach (var item in printSingle)
                            {
                                Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                            }
                            Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                            Console.WriteLine("Please select a number below:");
                            Console.WriteLine("_____________________________");
                            Console.WriteLine("1: Yes");
                            Console.WriteLine("2: Re-enter Invoice Entry ID");
                            Console.WriteLine("3: Exit back to Checks Menu");
                            bool decidePrint = int.TryParse(Console.ReadLine(), out finalPrint);
                            Console.Clear();
                            if (finalPrint == 1)
                            {
                                //Check Write
                                var openInv = RepoInvoices.GetOpenInvoice(invoiceEntryID);
                                double openAmount = 0;
                                foreach (var item in openInv)
                                {
                                    openAmount = item.Amount;
                                }
                                RepoChecks.InsertSingleCheck(employeeID, DateTime.Now.ToString("yyyy-MM-dd"), invoiceEntryID, openAmount);

                                //Change Invoice Table Status to PAID
                                RepoInvoices.UpdateSingleInvoiceStatus("PAID", invoiceEntryID);

                                //Show Check Printed
                                Console.WriteLine($"Check entry for Invoice Entry ID {invoiceEntryID}");
                                var single = RepoChecks.GetSingleCheck(invoiceEntryID);
                                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                                Console.WriteLine(String.Format("{0,-13} | {1, -7} | {2, -13} | {3, -13} | {4, -17} | {5, -13} | {6, -13} | {7, -13} | {8, -20} | {9, -8} | {10, -20} | {11, -18}", "Trans. Type", "Check #", "Check Date", "Inv. Entry ID", "Inv. Trans. Type", "Invoice #", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Printed By"));
                                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                                foreach (var item in single)
                                {
                                    Console.WriteLine(String.Format("{0,-13} | {1, -7} | {2, -13} | {3, -13} | {4, -17} | {5, -13} | {6, -13} | {7, -13} | {8, -20} | {9, -8} | {10, -20} | {11, -18}", item.TransactionType, item.CheckID, Convert.ToDateTime(item.CheckDate).ToString("yyyy-MM-dd"), item.InvoiceEntryID, item.InvoiceTransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName));
                                }
                                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                            }
                            if (finalPrint == 2)
                            {
                                redoSection = true;
                            }
                            if (finalPrint == 3)
                            {
                                redoSection = false;
                                redo = false;
                            }
                        } while (finalPrint != 1 && finalPrint != 2 && finalPrint != 3);
                    }
                } while (redoSection == true);

                bool redoSection2 = false;
                do
                {
                    redoSection2 = false;

                    if (checkWrite == 2)
                    {
                        int response = 0;
                        do
                        {
                            Console.WriteLine("_____________________________________________________________________________________________________");
                            Console.WriteLine("To print a batch of checks, you will need the invoice due date range you would like to print through.");
                            Console.WriteLine("_____________________________________________________________________________________________________");
                            Console.WriteLine("Please select a number below:");
                            Console.WriteLine("_____________________________");
                            Console.WriteLine("1: See open invoice list, ordered by due date");
                            Console.WriteLine("2: Enter Invoice Due Date Range");
                            bool invID = int.TryParse(Console.ReadLine(), out response);
                            Console.Clear();
                        } while (response != 1 && response != 2);

                        DateTime invDate1 = DateTime.MinValue;
                        DateTime invDate2 = DateTime.MinValue;

                        if (response == 1)
                        {
                            bool isDate;
                            do
                            {
                                var openSort = RepoInvoices.GetAllOpenInvoicesSortedDueDate();
                                Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                                Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Inv. Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                                Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                                foreach (var item in openSort)
                                {
                                    Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                                }
                                Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                                Console.WriteLine("Enter the date range you would like to print through");
                                Console.WriteLine("Please use format MM - DD - YYYY or YYYY - MM - DD");
                                Console.WriteLine("____________________________________________________");
                                Console.Write("Enter the first date here: ");
                                isDate = DateTime.TryParse(Console.ReadLine(), out invDate1);
                            } while (isDate == false);

                            bool isDate2;
                            do
                            {
                                Console.Write("Enter the first date here: ");
                                isDate2 = DateTime.TryParse(Console.ReadLine(), out invDate2);
                                Console.Clear();
                            } while (isDate2 == false);

                        }

                        if (response == 2)
                        {
                            bool isDate;
                            do
                            {
                                Console.WriteLine("____________________________________________________");
                                Console.WriteLine("Enter the date range you would like to print through");
                                Console.WriteLine("Please use format MM - DD - YYYY or YYYY - MM - DD");
                                Console.WriteLine("____________________________________________________");
                                Console.Write("Enter the first date here: ");
                                isDate = DateTime.TryParse(Console.ReadLine(), out invDate1);
                            } while (isDate == false);

                            bool isDate2;
                            do
                            {
                                Console.Write("Enter the first date here: ");
                                isDate2 = DateTime.TryParse(Console.ReadLine(), out invDate2);
                                Console.Clear();
                            } while (isDate2 == false);
                        }

                        string startDate = invDate1.ToString("yyyy-MM-dd");
                        string endDate = invDate2.ToString("yyyy-MM-dd");


                        int finalPrint = 0;
                        do
                        {
                            Console.WriteLine("______________________________________________________________________________________________________________________________");
                            Console.WriteLine("Are you sure you want to print the following invoices?");
                            Console.WriteLine("Please note, to remove or add checks, go back and update the invoice due date to fall in or out of the desired due date range.");
                            var openSorted = RepoInvoices.GetOpenDateRange(startDate, endDate);
                            Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                            Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Inv. Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                            Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                            foreach (var item in openSorted)
                            {
                                Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                            }
                            Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                            Console.WriteLine("Please select a number below:");
                            Console.WriteLine("_____________________________");
                            Console.WriteLine("1: Yes");
                            Console.WriteLine("2: Re-enter Invoice Due Date Range");
                            Console.WriteLine("3: Exit back to Checks Menu");
                            bool decidePrint = int.TryParse(Console.ReadLine(), out finalPrint);
                            Console.Clear();
                            if (finalPrint == 1)
                            {
                                //Insert Check Run (Multiple)
                                
                                RepoChecks.InsertChecks(startDate, endDate);

                                //Null Emp IDs from Checks table to get newly inserted checks
                                var nullEmp = RepoChecks.GetNullEmpIDChecks();
                                List<int> ckIDs = new List<int>();
                                foreach(var item in nullEmp)
                                {
                                    ckIDs.Add(item.CheckID);
                                }

                                //Iterate through list of Nulls to update Checks empID and check date
                                foreach (var item in ckIDs)
                                {
                                    RepoChecks.UpdateChecks(employeeID, DateTime.Now.ToString("yyyy-MM-dd"), item);
                                }

                                //Change Invoice Table Status to PAID
                                RepoInvoices.UpdateInvoiceStatus("PAID", startDate, endDate);

                                //Show Check Run. Requires Iteration through ckIDs above but will get the check id nums (first and last) and insert here.
                                Console.WriteLine($"Check entries for Invoices Due between {startDate} and {endDate}");
                                var checksComplete = RepoChecks.GetCheckRun(ckIDs[0], ckIDs[ckIDs.Count -1]);
                                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                                Console.WriteLine(String.Format("{0,-13} | {1, -7} | {2, -13} | {3, -13} | {4, -17} | {5, -13} | {6, -13} | {7, -13} | {8, -20} | {9, -8} | {10, -20} | {11, -18}", "Trans. Type", "Check #", "Check Date", "Inv. Entry ID", "Inv. Trans. Type", "Invoice #", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Printed By"));
                                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                                foreach (var item in checksComplete)
                                {
                                    Console.WriteLine(String.Format("{0,-13} | {1, -7} | {2, -13} | {3, -13} | {4, -17} | {5, -13} | {6, -13} | {7, -13} | {8, -20} | {9, -8} | {10, -20} | {11, -18}", item.TransactionType, item.CheckID, Convert.ToDateTime(item.CheckDate).ToString("yyyy-MM-dd"), item.InvoiceEntryID, item.InvoiceTransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName));
                                }
                                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                            }
                            if (finalPrint == 2)
                            {
                                redoSection2 = true;
                            }
                            if (finalPrint == 3)
                            {
                                redoSection2 = false;
                                redo = false;
                            }
                        } while (finalPrint != 1 && finalPrint != 2 && finalPrint != 3);
                    }
                } while (redoSection2 == true);

                if (redo == true)
                {
                    int end = 0;
                    do
                    {
                        Console.WriteLine("____________________________________");
                        Console.WriteLine("Would you like to print more checks?");
                        Console.WriteLine("____________________________________");
                        Console.WriteLine("Please select a number below:");
                        Console.WriteLine("____________________________________");
                        Console.WriteLine("1: Yes");
                        Console.WriteLine("2: Exit back to Checks Menu");
                        bool moreChecks = int.TryParse(Console.ReadLine(), out end);
                        Console.Clear();
                        if (end == 1)
                        {
                            redo = true;
                        }
                        if (end == 2)
                        {
                            redo = false;
                        }

                    } while (end != 1 && end != 2);
                }
            } while (redo == true);
   
        }

        public void ViewPrintedChecks()
        {
            
            Console.WriteLine("All Printed Checks:");
            var allCks = RepoChecks.GetAllChecks();
            Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
            Console.WriteLine(String.Format("{0,-13} | {1, -7} | {2, -13} | {3, -13} | {4, -17} | {5, -13} | {6, -13} | {7, -13} | {8, -20} | {9, -8} | {10, -20} | {11, -18}", "Trans. Type", "Check #", "Check Date", "Inv. Entry ID", "Inv. Trans. Type", "Invoice #", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Printed By"));
            Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
            foreach (var item in allCks)
            {
                Console.WriteLine(String.Format("{0,-13} | {1, -7} | {2, -13} | {3, -13} | {4, -17} | {5, -13} | {6, -13} | {7, -13} | {8, -20} | {9, -8} | {10, -20} | {11, -18}", item.TransactionType, item.CheckID, Convert.ToDateTime(item.CheckDate).ToString("yyyy-MM-dd"), item.InvoiceEntryID, item.InvoiceTransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName));
            }
            Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
            Console.WriteLine("To Exit back to the Checks Menu, press Enter.");

            Console.ReadLine();
            Console.Clear();
            
        }
    }
}
