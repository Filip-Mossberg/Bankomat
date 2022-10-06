using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;

namespace Bankomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int user = 0;

            // First place is a number of how many accounts you have. The second is PayrollAccount, third ShareAccount and last SavingsAccount. 
            double[] MoneyAccount0 = { 3, 403.25, 23400.83, 102420.63 }; // User 0
            double[] MoneyAccount1 = { 2, 1394.34, 43742.04 }; // User 1
            double[] MoneyAccount2 = { 1, 3493.52 };  // User 2
            double[] MoneyAccount3 = { 3, 1904.78, 18304.94, 74323.20 }; // User 3
            double[] MoneyAccount4 = { 2, 1452.86, 173744.14 }; // User 4

            for (int stage = 0; stage <= 2; stage++) // Tracks what stage of the bank we are on threw the variable stage.
            {
                if (stage != -1 && user != -2) // Checks so the user is allowed to be in the bank.
                {
                    switch (stage)
                    {
                        case 0:
                            Welcome();
                            break;
                        case 1:
                            string[,] accounts = Accounts();
                            user = Login(accounts);
                            break;
                        case 2:
                            int pick = menu();
                            switch (pick)
                            {
                                case 1:
                                    for (int count = 0; count <= 4; count++)
                                    {
                                        if (user == 0)
                                        {
                                            double[] MoneyAccount = MoneyAccount0;
                                            stage = AccountCheck0(MoneyAccount, user);
                                            break;
                                        }
                                        else if (user == 1)
                                        {
                                            double[] MoneyAccount = MoneyAccount1;
                                            stage = AccountCheck0(MoneyAccount, user);
                                            break;
                                        }
                                        else if (user == 2)
                                        {
                                            double[] MoneyAccount = MoneyAccount2;
                                            stage = AccountCheck0(MoneyAccount, user);
                                            break;
                                        }
                                        else if (user == 3)
                                        {
                                            double[] MoneyAccount = MoneyAccount3;
                                            stage = AccountCheck0(MoneyAccount, user);
                                            break;
                                        }
                                        else if (user == 4)
                                        {
                                            double[] MoneyAccount = MoneyAccount4;
                                            stage = AccountCheck0(MoneyAccount, user);
                                            break;
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                }
                else // Throws the user out of the bank.
                {
                    break;
                }
            }
        }
        public static void Welcome() // Writes out a random welome message.
        {
            String[] Welcome = { "Welcome to the bank!", "Welcome to your commercial bank!", "Welcome to your local bank!" };
            Random message = new Random();
            int number = message.Next(0, 3);
            Console.WriteLine(Welcome[number]);
        }
        public static string[,] Accounts() // Stores a 2d string array with accounts.
        {
            String[,] accounts = new string[5,2];
            accounts[0, 0] = "filip"; accounts[0, 1] = "2124";
            accounts[1, 0] = "noah"; accounts[1, 1] = "4452";
            accounts[2, 0] = "bella"; accounts[2, 1] = "9832";
            accounts[3, 0] = "joakim"; accounts[3, 1] = "2532";
            accounts[4, 0] = "ulrika"; accounts[4, 1] = "2435";
            return accounts;
        }
        public static int Login(string[,] accounts) // User Login, if successful the method returns the user position value in the array, if unsuccessful it returns -1.
        {
            try
            {
                bool check = false;
                int count = 0;
                for (int attempt = 0; attempt < 3; attempt++) // Counts how many times the user has tried loggin in.
                {
                    Console.Write("Enter your username:");
                    var name = Console.ReadLine().ToLower();
                    Console.Write("Enter your pin-code:");
                    var pincode = Console.ReadLine();

                    for (count = 0; count <= 4; count++) // Loop that goes threw all the accounts in the account array to try and find a match with the user input.
                    {
                        check = (name == accounts[count, 0] && pincode == accounts[count, 1]) ? true : false;
                        if (check == true) // If a match is found then it returns the user position value in the array.
                        {
                            int user = count;
                            return count;
                        }
                        else if (count == 4 && check == false) //If a match is not found the program tells the user that the input was wrong.
                        {
                            Console.WriteLine("Wrong username or pin-code!");
                            
                        }
                    }
                    if (attempt == 2 && check == false) // If the user has failed to login on 3 attempts then a value of -1 is returned. 
                    {
                        Console.WriteLine("To many attempts!");
                        return -2; // Have to return -2 because the for loop adds 1 value.
                    }
                }
                return -2; // Anny extra exception is captured here.
            }
            catch  (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -2; // Anny extra exception is captured here.
            }
        }
        public static int menu() // Lists the menue and lets the user pick a option on the menue.
        {
            string[] menue = { "\n1. See your accounts and balance", "2. Transfer between accounts", "3. Withdraw money", "4. Log out\n" };
            foreach(string list in menue) Console.WriteLine(list);
            
            for (int attempt = 0; attempt < 3; attempt++) // Counts how many times the user has tried to pick a option.
            {
                string pick = Console.ReadLine();
                for (int count = 0; count < 1; count++) // Loop that checks the user input to see if it matches with the options on the menue.
                {
                    try
                    {
                        if (pick == "1" || pick == "2" || pick == "3" || pick == "4")
                        {
                            return int.Parse(pick);
                        }
                        else 
                        {
                            Console.WriteLine("Invalid pick!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return -2; // Anny extra exception is captured here.
                    }
                }
                if (attempt == 2)
                {
                    Console.WriteLine("To many attempts!");
                    return -2; 
                }
            }
            return -1; // Anny extra exception is captured here.

        }
        private static int AccountCheck0(double[] MoneyAccount, int user) // Writes out a specific users accounts depending on how many account they have.
        {
            if (MoneyAccount[0] == 3) // User with 3 accounts.
            {
                StringBuilder user0 = new StringBuilder("Payroll account: ");
                user0.AppendFormat("{0:C}", MoneyAccount[1]);
                Console.WriteLine(user0.ToString());
                StringBuilder user00 = new StringBuilder("Share account: ");
                user00.AppendFormat("{0:C}", MoneyAccount[2]);
                Console.WriteLine(user00.ToString());
                StringBuilder user000 = new StringBuilder("Savings account: ");
                user000.AppendFormat("{0:C}", MoneyAccount[3]);
                Console.WriteLine(user000.ToString());
            }
            if (MoneyAccount[0] == 2) // User with 2 accounts.
            {
                StringBuilder user0 = new StringBuilder("Payroll account: ");
                user0.AppendFormat("{0:C}", MoneyAccount[1]);
                Console.WriteLine(user0.ToString());
                StringBuilder user00 = new StringBuilder("Share account: ");
                user00.AppendFormat("{0:C}", MoneyAccount[2]);
                Console.WriteLine(user00.ToString());
            }
            if (MoneyAccount[0] == 1) // User with 1 account.
            {
                StringBuilder user0 = new StringBuilder("Payroll account: ");
                user0.AppendFormat("{0:C}", MoneyAccount[1]);
                Console.WriteLine(user0.ToString());
            }

            Console.Write("\nPress Enter To Return To Menue:"); 
            for (int attempt = 0; attempt < 3; attempt++) // Loop taht returns the user to the menue if enter is pressed, if wrong input is typed more then 3 times the user is thrown back out of the bank.
            {
                try
                {
                    if (Console.ReadLine() == "")
                    {
                        int stage = 1;
                        return stage;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Pick!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -2; // Anny extra exception is captured here.
                }
                if (attempt == 2)
                {
                    Console.WriteLine("To many attempts!");
                    return -2;
                }
            }
            return -2; // Anny extra exception is captured here.

        }
    }
}
