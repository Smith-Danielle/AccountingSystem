using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace AccountingSystem
{
    public class EmployeeAccess
    {
        public EmployeeAccess()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            Repo1 = new DapperEmployeeRepository(conn);
        }

        public DapperEmployeeRepository Repo1 { get; set; }

        public int EmpID { get; set; }

        public void CreateAccount()
        {
            string response = string.Empty;
            int access;
            do
            {
                Console.WriteLine("To create and account, please enter the access code provided by your Administrator or type Exit to go back to the Login screen.");
                response = Console.ReadLine();
                Console.Clear();

                if (int.TryParse(response, out access))
                {
                    if (access != 1234)
                    {
                        Console.WriteLine("The access code you entered is incorrect.");
                    }
                }
                else
                {
                    if (response.ToLower() == "exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("The access code you entered is incorrect.");
                    }
                }
            } while (access != 1234);

            if (response != "exit")
            {
                Console.WriteLine("Whats your first name?");
                var first = Console.ReadLine();
                Console.WriteLine("Whats your last name?");
                var last = Console.ReadLine();
                Console.WriteLine("Whats your password?");
                var password = Console.ReadLine();
                Console.Clear();

                var empInfo = Repo1.GetEmployee(first, last);
                string passwordCheck = string.Empty;
                foreach (var item in empInfo)
                {
                    passwordCheck = item.Password;
                }
                if (String.IsNullOrEmpty(passwordCheck))
                {
                    Repo1.InsertEmployee(first, last, password);
                    Console.WriteLine("Your account has been created.");
                }
                else
                {
                    Console.WriteLine("You already have an account.");
                }
            } 
        }

        public void Login()
        {
            Console.WriteLine("To login, please enter your first name.");
            var first = Console.ReadLine();
            Console.WriteLine("Please enter your last name");
            var last = Console.ReadLine();
            Console.WriteLine("Please enter your password.");
            var passwordEntered = Console.ReadLine();
            Console.Clear();
            var empInfo = Repo1.GetEmployee(first, last);
            string password = string.Empty;
            int empID = 0;
            foreach (var item in empInfo)
            {
                password = item.Password;
                empID = item.EmployeeID;
            }
            if (String.IsNullOrEmpty(password))
            {

                Console.WriteLine("Sorry, you do not have an account.");
                //in the main program, dont move forward if EmpID is empty, ask employee if they would like to login or create account.

            }
            else
            {
                if (String.Equals(passwordEntered, password))
                {
                    EmpID = empID;
                    Console.WriteLine($"Hello {first}! You are now signed in.");
                    
                }
                else
                {
                    Console.WriteLine("The password you have entered is incorrect.");
                }
            }
        }
    }
}
