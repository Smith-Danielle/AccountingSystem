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
            //Please note, if accounts are added, update ReportsModule to reflect the newly added accounts


            /*EmployeeAccess empAccess = new EmployeeAccess();
            int login;
            bool run = true;
            do
            {
                Console.WriteLine("_____________________________________");
                Console.WriteLine("Welcome to AccountingSystems 2021");
                Console.WriteLine("_____________________________________");
                Console.WriteLine("Please select a number below to begin");
                Console.WriteLine("_____________________________________");
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
            
            if (run == true)
            {
                int module;
                bool runMain = true;
                do
                {
                    Console.WriteLine("_________");
                    Console.WriteLine("Main Menu");
                    Console.WriteLine("_________________________________________________");
                    Console.WriteLine("Please select a number below to access the Module");
                    Console.WriteLine("_________________________________________________");
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
                            moveforward = true;
                            InvoicesModule inv = new InvoicesModule();
                            Console.WriteLine("________");
                            Console.WriteLine("Invoices");
                            Console.WriteLine("____________________________");
                            Console.WriteLine("Please select a number below");
                            Console.WriteLine("____________________________");
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
                        int checkAction;
                        bool moveChecks = true;
                        do
                        {
                            moveChecks = true;
                            ChecksModule ck = new ChecksModule();
                            Console.WriteLine("______");
                            Console.WriteLine("Checks");
                            Console.WriteLine("____________________________");
                            Console.WriteLine("Please select a number below");
                            Console.WriteLine("____________________________");
                            Console.WriteLine("1: Write Checks");
                            Console.WriteLine("2: View Printed Checks");
                            Console.WriteLine("3: Exit back to Main Menu");
                            bool invMenu = int.TryParse(Console.ReadLine(), out checkAction);
                            Console.Clear();

                            if (checkAction == 1)
                            {
                                ck.CheckRun(empAccess.EmpID);
                            }
                            else if (checkAction == 2)
                            {
                                ck.ViewPrintedChecks();
                            }
                            else if (checkAction == 3)
                            {
                                moveChecks = false;
                            }

                        } while (moveChecks == true);
                    }
                    if (module == 3)
                    {
                        int depositAction;
                        bool moveDeposits = true;
                        do
                        {
                            moveDeposits = true;
                            DepositsModule dep = new DepositsModule();
                            Console.WriteLine("________");
                            Console.WriteLine("Deposits");
                            Console.WriteLine("____________________________");
                            Console.WriteLine("Please select a number below");
                            Console.WriteLine("____________________________");
                            Console.WriteLine("1: Write Checks");
                            Console.WriteLine("2: View Printed Checks");
                            Console.WriteLine("3: Exit back to Main Menu");
                            bool depMenu = int.TryParse(Console.ReadLine(), out depositAction);
                            Console.Clear();

                            if (depositAction == 1)
                            {
                                dep.EnterDeposit(empAccess.EmpID);
                            }
                            else if (depositAction == 2)
                            {
                                dep.ViewAllDeposits();
                            }
                            else if (depositAction == 3)
                            {
                                moveDeposits = false;
                            }

                        } while (moveDeposits == true);
                    }
                    if (module == 4)
                    {
                        int reportAction;
                        bool moveReports = true;
                        do
                        {
                            moveReports = true;
                            ReportsModule rep = new ReportsModule();
                            Console.WriteLine("_______");
                            Console.WriteLine("Reports");
                            Console.WriteLine("____________________________");
                            Console.WriteLine("Please select a number below");
                            Console.WriteLine("____________________________");
                            Console.WriteLine("1: View Account Activity & Balances");
                            Console.WriteLine("2: Check Net Income/Loss");
                            Console.WriteLine("3: Exit back to Main Menu");
                            bool repMenu = int.TryParse(Console.ReadLine(), out reportAction);
                            Console.Clear();

                            if (reportAction == 1)
                            {
                                rep.AccountActivity();
                            }
                            else if (reportAction == 2)
                            {
                                rep.NetBalance();
                            }
                            else if (reportAction == 3)
                            {
                                moveReports = false;
                            }

                        } while (moveReports == true);
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

            DapperReportsRepository rep = new DapperReportsRepository(conn);



            //Ask for account
            //list with id #
            //give opportunity to run a report for different account
            var accountname = 200;
            //be sure to format Account: Rent (400) 
            var accRep = rep.GetAccountActivity(accountname);
            Console.WriteLine($"Account: Cash ({accountname}) Detailed Transactions");
            Console.WriteLine("__________________________________________________________");
            Console.WriteLine(String.Format("{0,-17} | {1, -13} | {2, -10}", "Transaction Type", "Debit/Credit", "Amount"));
            Console.WriteLine("_______________________________________________");
            foreach (var item in accRep)
            {
                Console.WriteLine(String.Format("{0,-17} | {1, -13} | {2, -10}", item.TransactionType, item.Debit_Credit, item.Amount));
            }
            Console.WriteLine("_______________________________________________");
            Console.WriteLine("");
            Console.WriteLine("");
            var accCon = rep.GetCondensedActivity(accountname);
            Console.WriteLine($"Account: Cash ({accountname}) Condensed Balances");
            Console.WriteLine("_______________________________________________________");
            Console.WriteLine(String.Format("{0,-13} | {1, -10}", "Debit/Credit", "Amount"));
            Console.WriteLine("__________________________");
            foreach (var item in accCon)
            {
                Console.WriteLine(String.Format("{0,-13} | {1, -10}", item.Debit_Credit, item.Amount));
            }
            Console.WriteLine("__________________________");

            double debits = 0;
            double credits = 0;
            var accTotal = rep.GetCondensedActivity(accountname);
            foreach (var item in accTotal)
            {
                if (item.Debit_Credit == "Debit")
                {
                    debits = item.Amount;
                }
                if (item.Debit_Credit == "Credit")
                {
                    credits = item.Amount;
                }
            }

            double balance = 0;
            string accountType = string.Empty;

            if (accountname == 100 || accountname == 400 || accountname == 401 || accountname == 402 || accountname == 403)
            {
                balance = (debits - credits);
                accountType = $"Note: {accountname} is a Debit Balance Account.";
            }
            if (accountname == 200 || accountname == 300)
            {
                balance = (credits - debits);
                accountType = $"Note: {accountname} is a Credit Balance Account.";
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"Account: Cash ({accountname}) Total Balance");
            Console.WriteLine("__________________________________________________");
            Console.WriteLine($"{Math.Round(balance, 2)}");
            Console.WriteLine("___________");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(accountType);


            //Net Income/Loss
            /*Console.WriteLine("Net Income/Loss Report");
            var net = rep.GetNetBalance();
            Console.WriteLine("____________________________");
            Console.WriteLine(String.Format("{0,-15} | {1, -10}", "Revenue/Expense", "Amount"));
            Console.WriteLine("____________________________");
            foreach (var item in net)
            {
                Console.WriteLine(String.Format("{0,-15} | {1, -10}", item.Revenue_Expense, item.Amount));
            }
            Console.WriteLine("____________________________");

            double revenue = 0;
            double expense = 0;
            string type = string.Empty;
            double netNum = 0;
            var netBal = rep.GetNetBalance();
            foreach (var item in netBal)
            {
                if (item.Revenue_Expense == "Revenue")
                {
                    revenue = item.Amount;
                }
                if (item.Revenue_Expense == "Expense")
                {
                    expense = item.Amount;
                }
            }
            netNum = Math.Abs(revenue - expense);
            if (revenue >= expense)
            {
                type = "Net Income";
            }
            else
            {
                type = "Net Loss";
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("_____________________");
            Console.WriteLine($"{type}: {netNum}");
            Console.WriteLine("_____________________");*/



        }
    }
}
