using System;

namespace Bankomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Welcome();
            string[,] accounts = Accounts();
            Login();
        }
        public static void Welcome()
        {
            String[] Welcome = { "Welcome to the bank!", "Welcome to your commercial bank!", "Welcome to your local bank!" };
            Random message = new Random();
            int number = message.Next(0, 4);
            Console.WriteLine(Welcome[number]);
        }
        public static string[,] Accounts()
        {
            String[,] accounts = new string[5,2];
            accounts[0, 0] = "Filip"; accounts[0, 1] = "2124";
            accounts[1, 0] = "Noah"; accounts[1, 1] = "4452";
            accounts[2, 0] = "Bella"; accounts[2, 1] = "9832";
            accounts[3, 0] = "Joakim"; accounts[3, 1] = "2532";
            accounts[4, 0] = "Ulrika"; accounts[4, 1] = "2435";
            return accounts;
        }
        public static void Login()
        {

        }
    }
}
