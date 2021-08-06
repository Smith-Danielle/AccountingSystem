using System;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AccountingSystem
{
    public class ReportsModule
    {
        public ReportsModule()
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            RepoReports = new DapperReportsRepository(conn);
        }

        public DapperReportsRepository RepoReports { get; set; }

        public void AccountActivity()
        {
            bool redo = true;
            do
            {

                int accountID = 0;
                do
                {
                    Console.WriteLine("_________________________________________________________");
                    Console.WriteLine("To review a detailed account report, enter the Account ID");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("100: Cash");
                    Console.WriteLine("200: Accounts Payable");
                    Console.WriteLine("300: Revenue");
                    Console.WriteLine("400: Rent");
                    Console.WriteLine("401: Utilities");
                    Console.WriteLine("402: Repair & Maintenance");
                    Console.WriteLine("403: Office Supplies");
                    bool isAccount = int.TryParse(Console.ReadLine(), out accountID);
                    Console.Clear();
                } while (accountID != 100 && accountID != 200 && accountID != 300 && accountID != 400 && accountID != 401 && accountID != 402 && accountID != 403);

                string accountName = string.Empty;
                if (accountID == 100)
                {
                    accountName = "Cash";
                }
                if (accountID == 200)
                {
                    accountName = "Accounts Payable";
                }
                if (accountID == 300)
                {
                    accountName = "Revenue";
                }
                if (accountID == 400)
                {
                    accountName = "Rent";
                }
                if (accountID == 401)
                {
                    accountName = "Utilities";
                }
                if (accountID == 402)
                {
                    accountName = "Repair & Maintenance";
                }
                if (accountID == 403)
                {
                    accountName = "Office Supplies";
                }

                var accRep = RepoReports.GetAccountActivity(accountID);
                Console.WriteLine($"Account: {accountName} ({accountID}) Detailed Transactions");
                Console.WriteLine("__________________________________________________________");
                Console.WriteLine(String.Format("{0,-17} | {1, -13} | {2, -10}", "Transaction Type", "Debit/Credit", "Amount"));
                Console.WriteLine("_______________________________________________");
                foreach (var item in accRep)
                {
                    Console.WriteLine(String.Format("{0,-17} | {1, -13} | {2, -10}", item.TransactionType, item.Debit_Credit, Math.Round(item.Amount, 2)));
                }
                Console.WriteLine("_______________________________________________");
                Console.WriteLine("");
                Console.WriteLine("");
                var accCon = RepoReports.GetCondensedActivity(accountID);
                Console.WriteLine($"Account: {accountName} ({accountID}) Condensed Balances");
                Console.WriteLine("_______________________________________________________");
                Console.WriteLine(String.Format("{0,-13} | {1, -10}", "Debit/Credit", "Amount"));
                Console.WriteLine("__________________________");
                foreach (var item in accCon)
                {
                    Console.WriteLine(String.Format("{0,-13} | {1, -10}", item.Debit_Credit, Math.Round(item.Amount)));
                }
                Console.WriteLine("__________________________");

                double debits = 0;
                double credits = 0;
                var accTotal = RepoReports.GetCondensedActivity(accountID);
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

                if (accountID == 100 || accountID == 400 || accountID == 401 || accountID == 402 || accountID == 403)
                {
                    balance = (debits - credits);
                    accountType = $"Note: {accountName} ({accountID}) is a Debit Balance Account.";
                }
                if (accountID == 200 || accountID == 300)
                {
                    balance = (credits - debits);
                    accountType = $"Note: {accountName} ({accountID}) is a Credit Balance Account.";
                }

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"Account: {accountName} ({accountID}) Total Balance");
                Console.WriteLine("__________________________________________________");
                Console.WriteLine($"{Math.Round(balance, 2)}");
                Console.WriteLine("___________");

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(accountType);
                Console.WriteLine("Press Enter to close report");
                

                int end = 0;
                do
                {
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("________________________________________________");
                    Console.WriteLine("Would you like to review another account report?");
                    Console.WriteLine("________________________________________________");
                    Console.WriteLine("Please select a number below:");
                    Console.WriteLine("_____________________________");
                    Console.WriteLine("1: Yes");
                    Console.WriteLine("2: Exit back to Reports Menu");
                    bool anotherReport = int.TryParse(Console.ReadLine(), out end);
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
            } while (redo == true);
        }

        public void NetBalance()
        {
            Console.WriteLine("Net Income/Loss Report");
            var net = RepoReports.GetNetBalance();
            Console.WriteLine("____________________________");
            Console.WriteLine(String.Format("{0,-15} | {1, -10}", "Revenue/Expense", "Amount"));
            Console.WriteLine("____________________________");
            foreach (var item in net)
            {
                Console.WriteLine(String.Format("{0,-15} | {1, -10}", item.Revenue_Expense, Math.Round(item.Amount, 2)));
            }
            Console.WriteLine("____________________________");

            double revenue = 0;
            double expense = 0;
            string type = string.Empty;
            double netNum = 0;
            var netBal = RepoReports.GetNetBalance();
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
            Console.WriteLine($"{type}: {Math.Round(netNum, 2)}");
            Console.WriteLine("_____________________");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("To Exit back to the Reports Menu, press Enter.");

            Console.ReadLine();
            Console.Clear();
        }
    }
}
