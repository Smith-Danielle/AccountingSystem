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

            EmployeeAccess run = new EmployeeAccess();
            run.Login();
           





            /*var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            var repo1 = new DapperEmployeeRepository(conn);

            Console.WriteLine("Lets create an account");
            Console.WriteLine("Whats your first name?");
            var first = Console.ReadLine();
            Console.WriteLine("Whats your last name?");
            var last = Console.ReadLine();
            Console.WriteLine("Whats your password?");
            var password = Console.ReadLine();
            repo1.InsertEmployee(first, last, password);

            Console.WriteLine("Now let's sign in");
            Console.WriteLine("Whats your first name?");
            var first1 = Console.ReadLine();
            Console.WriteLine("Whats your last name?");
            var last1 = Console.ReadLine();
            Console.WriteLine("Whats your password?");
            var password1 = Console.ReadLine();
            var savedPassword = repo1.GetEmployee(first1, last1);
            string saved = string.Empty;
            foreach (var item in savedPassword)
            {
                saved = item.Password;
            }
            if (String.IsNullOrEmpty(saved))
            {
                Console.WriteLine("Sorry, you do not have an account.");
            }
            else
            {
                if (String.Equals(password1, saved))
                {
                    Console.WriteLine($"Hello {first1}! Thanks for signing in.");
                }
                else
                {
                    Console.WriteLine("Password does not match");
                }
            }*/

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            var repo1 = new DapperInvoiceRepository(conn);
            
            Console.WriteLine("Enter: Invoice or Credit Memo");
            string transactionType = Console.ReadLine();
            Console.WriteLine("Enter: Invoice Number");
            string invoiceNumber = Console.ReadLine();
            Console.WriteLine("Enter: Invoice Date");
            string invoiceDate = Convert.ToDateTime(Console.ReadLine()).ToString("yyyy-MM-dd");
            Console.WriteLine("Enter: Due Date");
            string dueDate = Convert.ToDateTime(Console.ReadLine()).ToString("yyyy-MM-dd");
            Console.WriteLine("Enter: Vendor Name");
            string vendorName = Console.ReadLine();
            Console.WriteLine("Enter: Amount");
            double amount = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter: Account Number");
            Console.WriteLine("400: Rent");
            Console.WriteLine("401: Utilites");
            Console.WriteLine("402: Repair & Maintenance");
            Console.WriteLine("403: Office Supplies");
            int accountIdDebit = Convert.ToInt32(Console.ReadLine());

            repo1.InsertInvoice(run.EmpID, transactionType, DateTime.Now.ToString("yyyy-MM-dd"), invoiceNumber, invoiceDate, dueDate, vendorName, amount, accountIdDebit);
        }
    }
}
