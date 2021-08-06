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
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("Invoice or Credit Memo");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("1: Invoice");
                    Console.WriteLine("2: Credit Memo");
                    bool question = int.TryParse(Console.ReadLine(), out choice);
                    Console.Clear();
                } while (choice != 1 && choice != 2);
                if (choice == 1)
                {
                    transactionType = "INVOICE";
                }
                else if (choice == 2)
                {
                    transactionType = "CREDIT MEMO";
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
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("Enter: Account Number");
                    Console.WriteLine("_____________________________");
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
                    Console.WriteLine("________________________________________");
                    Console.WriteLine("Would you like to enter another invoice?");
                    Console.WriteLine("________________________________________");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("________________________________________");
                    Console.WriteLine("1: Enter additional invoice");
                    Console.WriteLine("2: Exit back to Invoices Menu");
                    bool action = int.TryParse(Console.ReadLine(), out nextAction);
                    Console.Clear();
                } while (nextAction != 1 && nextAction != 2);

            } while (nextAction == 1);
        }
        public void ViewOpenInvoices()
        {
            int nextAction;
            do
            {
                int order;
                do
                {
                    Console.WriteLine("__________________________________________________________________________");
                    Console.WriteLine("Would you like to view open invoies sorted by Invoice Entry ID or Due Date");
                    Console.WriteLine("__________________________________________________________________________");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("1: Order by Invoice Entry ID");
                    Console.WriteLine("2: Order by Due Date");
                    bool orderType = int.TryParse(Console.ReadLine(), out order);
                    Console.Clear();
                } while (order != 1 && order != 2);

                if (order == 1)
                {
                    var openInvoices = RepoInvoice.GetAllOpenInvoices();
                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
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
                    Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
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
                    Console.WriteLine("_______________________________________________________");
                    Console.WriteLine("Would you like view open invoices in a different order?");
                    Console.WriteLine("_______________________________________________________");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("1: View open invoice in a different order");
                    Console.WriteLine("2: Exit back to Invoices Menu");
                    bool action = int.TryParse(Console.ReadLine(), out nextAction);
                    Console.Clear();
                } while (nextAction != 1 && nextAction != 2);

            } while (nextAction == 1);
        }
        public void UpdateInvoice()
        {
            int section = 0;
            bool backToInvoices = true;
            do
            {
                int response;
                bool update;
                do
                {
                    Console.WriteLine("_________________________________________________________");
                    Console.WriteLine("To update an invoice, you will need the Invoice Entry ID.");
                    Console.WriteLine("_________________________________________________________");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("1: See open invoice list");
                    Console.WriteLine("2: Enter Invoice Entry ID");
                    update = int.TryParse(Console.ReadLine(), out response);
                    Console.Clear();
                } while (response != 1 && response != 2);

                int invoiceEntryID = 0;

                if (response == 1)
                {
                    int response1;
                    bool order;
                    do
                    {
                        Console.WriteLine("Would you like to see a list sorted by Invoice Entry ID or Due Date?");
                        Console.WriteLine("Please select a number below:");
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
                            var open = RepoInvoice.GetAllOpenInvoices();
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

                            var openInvoices = RepoInvoice.GetOpenInvoice(invoiceEntryID);

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
                            var openSort = RepoInvoice.GetAllOpenInvoicesSortedDueDate();
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

                            var openInvoices1 = RepoInvoice.GetOpenInvoice(invoiceEntryID);

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
                        var openInvoices1 = RepoInvoice.GetOpenInvoice(invoiceEntryID);

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

                bool sameID = true;
                do
                {
                    sameID = true;
                    int updateType;
                do
                {
                    Console.WriteLine("______________________________");
                    Console.WriteLine("What would you like to update?");
                    Console.WriteLine("______________________________");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("______________________________");
                    Console.WriteLine("1: Transaction Type");
                    Console.WriteLine("2: Invoice Number");
                    Console.WriteLine("3: Invoice Date");
                    Console.WriteLine("4: Due Date");
                    Console.WriteLine("5: Vendor Name");
                    Console.WriteLine("6: Amount");
                    Console.WriteLine("7: Account");
                    bool typeEntry = int.TryParse(Console.ReadLine(), out updateType);
                    Console.Clear();
                } while (updateType != 1 && updateType != 2 && updateType != 3 && updateType != 4 && updateType != 5 && updateType != 6 && updateType != 7);

                    if (updateType == 1)
                    {
                        int choice;
                        do
                        {
                            Console.WriteLine("__________________________________________________________");
                            Console.WriteLine("Enter the updated Transation Type. Invoice or Credit Memo.");
                            Console.WriteLine("__________________________________________________________");
                            Console.WriteLine("Please select a number below:");
                            Console.WriteLine("_____________________________");
                            Console.WriteLine("1: Invoice");
                            Console.WriteLine("2: Credit Memo");
                            bool question = int.TryParse(Console.ReadLine(), out choice);
                            Console.Clear();
                        } while (choice != 1 && choice != 2);

                        if (choice == 1)
                        {
                            RepoInvoice.UpdateInvoiceTransactionType(invoiceEntryID, "INVOICE");
                        }
                        else if (choice == 2)
                        {
                            RepoInvoice.UpdateInvoiceTransactionType(invoiceEntryID, "CREDIT MEMO");
                        }
                    }

                    if (updateType == 2)
                    {
                        Console.WriteLine("Enter the updated Invoice Number.");
                        string invoice = Console.ReadLine();
                        Console.Clear();
                        RepoInvoice.UpdateInvoiceNumber(invoiceEntryID, invoice);
                    }

                    if (updateType == 3)
                    {

                        DateTime invDate;
                        bool isDate;
                        do
                        {
                            Console.WriteLine("Enter the updated Invoice Date");
                            Console.WriteLine("Please use format MM-DD-YYYY or YYYY-MM-DD");
                            isDate = DateTime.TryParse(Console.ReadLine(), out invDate);
                            Console.Clear();
                        } while (isDate == false);
                        string invoiceDate = invDate.ToString("yyyy-MM-dd");
                        RepoInvoice.UpdateInvoiceDate(invoiceEntryID, invoiceDate);
                    }

                    if (updateType == 4)
                    {

                        DateTime dDate;
                        bool isDue;
                        do
                        {
                            Console.WriteLine("Enter the updated Due Date");
                            Console.WriteLine("Please use format MM-DD-YYYY or YYYY-MM-DD");
                            isDue = DateTime.TryParse(Console.ReadLine(), out dDate);
                            Console.Clear();
                        } while (isDue == false);
                        string dueDate = dDate.ToString("yyyy-MM-dd");
                        RepoInvoice.UpdateDueDate(invoiceEntryID, dueDate);
                    }

                    if (updateType == 5)
                    {
                        Console.WriteLine("Enter the updated Vendor Name");
                        string vendor = Console.ReadLine();
                        Console.Clear();
                        RepoInvoice.UpdateVendorName(invoiceEntryID, vendor);
                    }

                    if (updateType == 6)
                    {
                        bool num;
                        double amount;
                        do
                        {
                            Console.WriteLine("Enter the updated Amount");
                            num = double.TryParse(Console.ReadLine(), out amount);
                            Console.Clear();
                        } while (num == false);
                        RepoInvoice.UpdateAmount(invoiceEntryID, amount);
                    }

                    if (updateType == 7)
                    {
                        int account;
                        do
                        {
                            Console.WriteLine("________________________________");
                            Console.WriteLine("Enter the updated Account Number");
                            Console.WriteLine("________________________________");
                            Console.WriteLine("400: Rent");
                            Console.WriteLine("401: Utilites");
                            Console.WriteLine("402: Repair & Maintenance");
                            Console.WriteLine("403: Office Supplies");
                            bool accNum = int.TryParse(Console.ReadLine(), out account);
                            Console.Clear();
                        } while (account != 400 && account != 401 && account != 402 && account != 403);
                        RepoInvoice.UpdateAccount(invoiceEntryID, account);
                    }
                    do
                    {
                        Console.WriteLine("________________________________________________________________________________");
                        Console.WriteLine($"Would you like to update additional info for Invoice Entry ID: {invoiceEntryID}");
                        Console.WriteLine("________________________________________________________________________________");
                        Console.WriteLine("Please select a number below");
                        Console.WriteLine("____________________________");
                        Console.WriteLine($"1: Update additional info for Invoice Entry ID: {invoiceEntryID}");
                        Console.WriteLine("2: Update a new Invoice Entry ID");
                        Console.WriteLine("3: Exit back to Invoices Menu");
                        bool redo = int.TryParse(Console.ReadLine(), out section);
                        Console.Clear();
                        if (section == 1)
                        {
                            sameID = true;
                        }
                        if (section == 2)
                        {
                            sameID = false;
                        }
                        if (section == 3)
                        {
                            sameID = false;
                            backToInvoices = false;
                        }
                    } while (section != 1 && section != 2 && section != 3);

                } while (sameID == true);

            } while (backToInvoices == true);

        }

        public void DeleteInvoice()
        {
            bool redo = true;
            do
            {
                bool redoSection = false;
                do
                {
                    redoSection = false;
                    int response;
                    do
                    {
                        Console.WriteLine("_________________________________________________________");
                        Console.WriteLine("To delete an invoice, you will need the Invoice Entry ID.");
                        Console.WriteLine("_________________________________________________________");
                        Console.WriteLine("Please select a number below:");
                        Console.WriteLine("_____________________________");
                        Console.WriteLine("1: See open invoice list");
                        Console.WriteLine("2: Enter Invoice Entry ID");
                        bool deleteOption = int.TryParse(Console.ReadLine(), out response);
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
                                var open = RepoInvoice.GetAllOpenInvoices();
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

                                var openInvoices = RepoInvoice.GetOpenInvoice(invoiceEntryID);

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
                                var openSort = RepoInvoice.GetAllOpenInvoicesSortedDueDate();
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

                                var openInvoices1 = RepoInvoice.GetOpenInvoice(invoiceEntryID);

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
                            var openInvoices1 = RepoInvoice.GetOpenInvoice(invoiceEntryID);

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

                    int finalDelete = 0;
                    do
                    {
                        Console.WriteLine("Are you sure you want to delete the following invoice?");
                        var delete = RepoInvoice.GetOpenInvoice(invoiceEntryID);
                        Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                        Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", "Status", "Inv. Entry ID", "Transaction Type", "Invoice Number", "Invoice Date", "Due Date", "Vendor Name", "Amount", "Account", "Entered By", "Entry Date"));
                        Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                        foreach (var item in delete)
                        {
                            Console.WriteLine(String.Format("{0,-6} | {1, -13} | {2, -17} | {3, -15} | {4, -13} | {5, -13} | {6, -24} | {7, -10} | {8, -22} | {9, -19} | {10, -13}", item.Status, item.InvoiceEntryID, item.TransactionType, item.InvoiceNumber, Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd"), Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd"), item.VendorName, item.Amount, item.AccountName, item.EmployeeName, Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd")));
                        }
                        Console.WriteLine("__________________________________________________________________________________________________________________________________________________________________________________________________");
                        Console.WriteLine("Please select a number below:");
                        Console.WriteLine("_____________________________");
                        Console.WriteLine("1: Yes");
                        Console.WriteLine("2: Re-enter Invoice Entry ID");
                        Console.WriteLine("3: Exit back to Invoices Menu");
                        bool decideDelete = int.TryParse(Console.ReadLine(), out finalDelete);
                        Console.Clear();
                        if (finalDelete == 1)
                        {
                            RepoInvoice.DeleteInvoice(invoiceEntryID);
                        }
                        if (finalDelete == 2)
                        {
                            redoSection = true;
                        }
                        if (finalDelete == 3)
                        {
                            redoSection = false;
                            redo = false;
                        }
                    } while (finalDelete != 1 && finalDelete != 2 && finalDelete != 3);

                } while (redoSection == true);
                if (redo == true)
                {
                    int end = 0;
                    do
                    {
                        Console.WriteLine("_________________________________________");
                        Console.WriteLine("Would you like to delete another invoice?");
                        Console.WriteLine("_________________________________________");
                        Console.WriteLine("Please select a number below:");
                        Console.WriteLine("_____________________________");
                        Console.WriteLine("1: Yes");
                        Console.WriteLine("2: Exit back to Invoices Menu");
                        bool anotherDelete = int.TryParse(Console.ReadLine(), out end);
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
    }
}
