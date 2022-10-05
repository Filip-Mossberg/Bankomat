using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http;
using System.Reflection.Metadata;

namespace Bankomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool program = true;
            startprogram(program);
            Welcome();
            string[,] accounts = Accounts();
            int user = Login(accounts);
            Console.WriteLine(user);
        }
        public static bool startprogram(bool program)
        {
            program = (program == true) ? true : false;
            return program;
        }
        public static void Welcome()
        {
            String[] Welcome = { "Welcome to the bank!", "Welcome to your commercial bank!", "Welcome to your local bank!" };
            Random message = new Random();
            int number = message.Next(0, 3);
            Console.WriteLine(Welcome[number]);
        }
        public static string[,] Accounts()
        {
            String[,] accounts = new string[5,2];
            accounts[0, 0] = "filip"; accounts[0, 1] = "2124";
            accounts[1, 0] = "noah"; accounts[1, 1] = "4452";
            accounts[2, 0] = "bella"; accounts[2, 1] = "9832";
            accounts[3, 0] = "joakim"; accounts[3, 1] = "2532";
            accounts[4, 0] = "ulrika"; accounts[4, 1] = "2435";
            return accounts;
        }
        public static int Login(string[,] accounts)
        {
            try
            {
                bool check = false;
                int count = 0;
                for (int attempt = 0; attempt < 3; attempt++)
                {
                    Console.Write("Enter your username:");
                    var name = Console.ReadLine();
                    Console.Write("Enter your pin-code:");
                    var pincode = Console.ReadLine();
                    for (count = 0; count <= 4; count++)
                    {
                        check = (name == accounts[count, 0] && pincode == accounts[count, 1]) ? true : false;
                        if (check == true)
                        {
                            int user = count;
                            return count;
                        }
                        else if (count == 4 && check == false)
                        {
                            Console.WriteLine("Wrong username or pin-code!");
                            
                        }
                    }
                    if (attempt == 2 && check == false)
                    {
                        Console.WriteLine("To many attempts!");
                        return -1;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}
