using System;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AccountingSystem
{
    public class DepositsModule
    {
        public DepositsModule()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            RepoDeposits = new DapperDepositsRepository(conn);
        }

        public DapperDepositsRepository RepoDeposits{ get; set; }

        public void EnterDeposit(int employeeID)
        {
            bool redo = true;
            do
            { 

                bool redoSection = false;
                do
                {
                    redoSection = false;
                    bool num;
                    double amount;
                    do
                    {
                        Console.WriteLine("Enter: Amount");
                        num = double.TryParse(Console.ReadLine(), out amount);
                        Console.Clear();
                    } while (num == false);

                    int finalDeposit = 0;
                    do
                    {
                        Console.WriteLine($"Confirm deposit entry for {amount}");
                        Console.WriteLine("Please select a number below:");
                        Console.WriteLine("_____________________________");
                        Console.WriteLine("1: Yes");
                        Console.WriteLine("2: No, re-enter deposit amount");
                        Console.WriteLine("3: Exit back to Deposits Menu");
                        bool decideDeposit = int.TryParse(Console.ReadLine(), out finalDeposit);
                        Console.Clear();

                        if (finalDeposit == 1)
                        {
                            RepoDeposits.InsertDeposit(employeeID, DateTime.Now.ToString("yyyy-MM-dd"), amount);
                            Console.WriteLine($"A deposit for {amount} has been entered.");
                        }
                        if (finalDeposit == 2)
                        {
                            redoSection = true;
                        }
                        if (finalDeposit == 3)
                        {
                                redoSection = false;
                                redo = false;
                        }
                    } while (finalDeposit != 1 && finalDeposit != 2 && finalDeposit != 3);
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
                        Console.WriteLine("2: Exit back to Deposits Menu");
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

        public void ViewAllDeposits()
        {

            Console.WriteLine("All Deposits:");
            var allDeps = RepoDeposits.GetAllDeposits();
            Console.WriteLine("_______________________________________________________________________________________");
            Console.WriteLine(String.Format("{0, -17} | {1, -15} | {2, -13} | {3, -10} | {4, -22}", "Transaction Type", "Deposit Number", "Deposit Date", "Amount", "Deposited By"));
            Console.WriteLine("_______________________________________________________________________________________");
            foreach (var item in allDeps)
            {
                Console.WriteLine(String.Format("{0, -17} | {1, -15} | {2, -13} | {3, -10} | {4, -22}", item.TransactionType, item.DepositID, Convert.ToDateTime(item.DepositDate).ToString("yyyy-MM-dd"), item.Amount, item.EmployeeName));
            }
            Console.WriteLine("_______________________________________________________________________________________");
            Console.WriteLine("To Exit back to the Deposits Menu, press Enter.");
        }
    }
}
