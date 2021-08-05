using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

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
            /*
            // Main Menu access still needs to have empAccess.EmpID value for entry methods (insert intos)
            if (run == true)
            {
                int module;
                bool runMain = true;
                do
                {
                    Console.WriteLine("Main Menu");
                    Console.WriteLine("Please select a number below to access the Module");
                    Console.WriteLine("1: Invoices");
                    Console.WriteLine("2: Checks");
                    Console.WriteLine("3: Deposits");
                    Console.WriteLine("4: Reports");
                    Console.WriteLine("5: Exit");
                    bool mainMenu = int.TryParse(Console.ReadLine(), out module);
                    Console.Clear();

                    if (module == 1)
                    {
                        int invoiceAction;
                        bool moveforward = true;
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
                            bool invMenu = int.TryParse(Console.ReadLine(), out invoiceAction);
                            Console.Clear();

                            if (invoiceAction == 1)
                            {
                                inv.InvoiceEntry(empAccess.EmpID);
                            }
                            else if (invoiceAction == 2)
                            {
                                inv.ViewOpenInvoices();
                            }
                            else if (invoiceAction == 3)
                            {
                                inv.UpdateInvoice();
                            }
                            else if (invoiceAction == 4)
                            {
                                inv.DeleteInvoice();
                            }
                            else if (invoiceAction == 5)
                            {
                                moveforward = false;
                            }
                        } while (moveforward == true);
                    }

                    if (module == 2)
                    {

                    }
                    if (module == 3)
                    {

                    }
                    if (module == 4)
                    {

                    }
                    if (module == 5)
                    {
                        runMain = false;
                    }
                } while (empAccess.EmpID != 0 && runMain == true);
            }
            Console.WriteLine("Thanks for using AccountingSystems 2021");*/

            var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            DapperInvoiceRepository inv = new DapperInvoiceRepository(conn);
            DapperChecksRepository ck = new DapperChecksRepository(conn);





            //Single Check
            // Similar to Update, make sure its open //Refer to update to check on how to check status
            /*int invoiceEntryID = 2; //enter by user
            var open = inv.GetOpenInvoice(invoiceEntryID);
            double amount = 0;
            foreach (var item in open)
            {
                amount = item.Amount;
            }
            //Check Method need to have a empid constructor like insert invoice when you create check module
            ck.InsertSingleCheck(empAccess.EmpID, DateTime.Now.ToString("yyyy-MM-dd"), invoiceEntryID, amount);*/

            //Change Single Invoice Status to Paid
            //InvoiceID will be variable from user input
            //inv.UpdateSingleInvoiceStatus("PAID", 3);

            //Show Check Printed
            /*int invoiceEntryID = 2; //enter by user
            var single = ck.GetSingleCheck(invoiceEntryID);
            foreach (var item in single)
            {
                Console.WriteLine($"{item.TransactionType}, {item.CheckID}, {Convert.ToDateTime(item.CheckDate).ToString("yyyy-MM-dd")}, {item.Amount}, {item.InvoiceEntryID}, {item.AccountName}, {item.EmployeeName}, {item.VendorName}, {Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd")}, {item.InvoiceNumber}, {Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd")}, {item.InvoiceTransactionType}"); 
            }*/



            //Insert Check Run (Multiple)
            //User will put in date range
            /*string start = "2021-09-22";
            string end = "2021-10-01";
            ck.InsertChecks(start, end);*/

            //null emp ids
            /*var nullEmp = ck.GetNullEmpIDChecks();
            List<int> ckIDs = new List<int>();
            foreach(var item in nullEmp)
            {
                ckIDs.Add(item.CheckID);
            }*/


            //iterate through list to update
            /*int empID = 6;
            string date = "2021-08-05";
            foreach (var item in ckIDs)
            {
                ck.UpdateChecks(empID, date, item);
            }*/

            //Change Invoice Status to Paid
            //Dates will be variables from user input
            //inv.UpdateInvoiceStatus("PAID", "2021-09-01", "2021-09-06");


            //Get check run. Requires iteration through null emp id list and get the check id nums (first and last) and insert here.
            //See ckIDs above
            /*var checksComplete = ck.GetCheckRun(1, 2);
            foreach (var item in checksComplete)
            {
                Console.WriteLine($"{item.TransactionType}, {item.CheckID}, {Convert.ToDateTime(item.CheckDate).ToString("yyyy-MM-dd")}, {item.Amount}, {item.InvoiceEntryID}, {item.AccountName}, {item.EmployeeName}, {item.VendorName}, {Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd")}, {item.InvoiceNumber}, {Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd")}, {item.InvoiceTransactionType}");
            }*/

            //Get all checks
            //Need to actually do a inner join to get all these tables
            /*var allChecks = ck.GetAllChecks();
            foreach (var item in allChecks)
            {
                Console.WriteLine($"{item.TransactionType}, {item.CheckID}, {Convert.ToDateTime(item.CheckDate).ToString("yyyy-MM-dd")}, {item.Amount}, {item.InvoiceEntryID}, {item.AccountName}, {item.EmployeeName}, {item.VendorName}, {Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd")}, {item.InvoiceNumber}, {Convert.ToDateTime(item.InvoiceDate).ToString("yyyy-MM-dd")}, {item.InvoiceTransactionType}");

            }*/








        }
}
}
