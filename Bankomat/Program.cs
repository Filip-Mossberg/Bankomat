using Microsoft.VisualBasic;
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
            double[][] MoneyAccount = new double[5][];
            MoneyAccount[0] = new double[3] { 403.25, 23400.83, 102420.63 };
            MoneyAccount[1] = new double[2] { 1394.34, 43742.04 };
            MoneyAccount[2] = new double[1] { 3493.52 };
            MoneyAccount[3] = new double[3] { 1904.78, 18304.94, 74323.20 };
            MoneyAccount[4] = new double[2] { 1452.86, 173744.14 };

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
                            int pick = menu(user);
                            switch (pick)
                            {
                                case 1:
                                    for (int count = 0; count <= 4; count++)
                                    {
                                        if (user == count)
                                        {
                                            AccountCheck(MoneyAccount[count]);
                                            stage = Return();
                                        };
                                    }
                                    break;
                                case 2:
                                    for (int count = 0; count <= 4; count++)
                                    {
                                        if (user == count)
                                        {
                                            AccountCheck(MoneyAccount[count]);
                                            stage = Transfer(MoneyAccount[count], user);
                                            if (stage == -2) { break; }
                                            else { AccountCheck(MoneyAccount[count]); stage = Return(); }
                                        };
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
            String[] Welcome = { "Welcome to the bank!\n", "Welcome to your commercial bank!\n", "Welcome to your local bank!\n" };
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
                            Console.Clear();
                            Console.WriteLine("Wrong username or pin-code!\n");
                            
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
        public static int menu(int user) // Lists the menu and lets the user pick a option on the menue.
        {
            for (int WelcomeMessage = 0; WelcomeMessage < 1; WelcomeMessage++)
            {
                Console.Clear();
                switch (user)
                {
                    case 0:
                        Console.WriteLine("Welcome Filip!");
                        break;
                    case 1:
                        Console.WriteLine("Welcome Noah!");
                        break;
                    case 2:
                        Console.WriteLine("Welcome Bella!");
                        break;
                    case 3:
                        Console.WriteLine("Welcome Joakim!");
                        break;
                    case 4:
                        Console.WriteLine("Welcome Ulrika!");
                        break;
                }
            }
            string[] menue = { "\n1. See your accounts and balance", "2. Transfer between accounts", "3. Withdraw money", "4. Log out\n" };
            foreach(string list in menue) Console.WriteLine(list);
            
            for (int attempt = 0; attempt < 3; attempt++) // Counts how many times the user has tried to pick a option.
            {
                string pick = Console.ReadLine();
                for (int count = 0; count < 1; count++) // Loop that checks the user input to see if it matches with the options on the menu.
                {
                    try
                    {
                        if (pick == "1" || pick == "2" || pick == "3" || pick == "4")
                        {
                            return int.Parse(pick);
                        }
                        else 
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid pick!\n");
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
            return -2; // Anny extra exception is captured here.

        }
        private static void AccountCheck(double[] MoneyAccount) // Writes out a specific users accounts depending on how many account they have.
        {
            Console.Clear();
            if (MoneyAccount.Length == 1) // If the user has 1 account.
            {
                StringBuilder user0 = new StringBuilder("[1] Payroll account: ");
                user0.AppendFormat("{0:C}", MoneyAccount[0]);
                Console.WriteLine(user0.ToString());
            }
            if (MoneyAccount.Length == 2) // If the user has 2 accounts.
            {
                StringBuilder user0 = new StringBuilder("[1] Payroll account: ");
                user0.AppendFormat("{0:C}", MoneyAccount[0]);
                Console.WriteLine(user0.ToString());
                StringBuilder user00 = new StringBuilder("[2] Share account: ");
                user00.AppendFormat("{0:C}", MoneyAccount[1]);
                Console.WriteLine(user00.ToString());
            }
            if (MoneyAccount.Length == 3) // If the user has 3 accounts.
            {
                StringBuilder user0 = new StringBuilder("[1] Payroll account: ");
                user0.AppendFormat("{0:C}", MoneyAccount[0]);
                Console.WriteLine(user0.ToString());
                StringBuilder user00 = new StringBuilder("[2] Share account: ");
                user00.AppendFormat("{0:C}", MoneyAccount[1]);
                Console.WriteLine(user00.ToString());
                StringBuilder user000 = new StringBuilder("[3] Savings account: ");
                user000.AppendFormat("{0:C}", MoneyAccount[2]);
                Console.WriteLine(user000.ToString());
            }

        }
        public static int Return()
        {
            Console.WriteLine("\nPress Enter To Return To Menue:");
            for (int attempt = 0; attempt < 3; attempt++) // Loop taht returns the user to the menu if enter is pressed, if wrong input is typed more then 3 times the user is thrown back out of the bank.
            {
                try
                {
                    if (Console.ReadLine() == "")
                    {
                        return 1;
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
        public static int Transfer(double[] MoneyAccount, int user)
        {
            try
            {
                int From = 0;
                int To = 0;
                double amount = Math.Round(0.00, 2);
                int break1 = 0;
                
                for (int attempt = 0; attempt <= 2; attempt++)
                {
                    Console.Write("\nTransfer From:");
                    From = int.Parse(Console.ReadLine());
                    if (From > 0 && From <= MoneyAccount.Length)
                    {
                        break;
                    }
                    else if (attempt == 2)
                    {
                        Console.WriteLine("To many attempts!");
                        return -2;
                    }
                }
                for (int attempt = 0; attempt <= 2; attempt++)
                {
                    Console.Write("\nTransfer To:");
                    To = int.Parse(Console.ReadLine()); 
                    if (To > 0 && To <= MoneyAccount.Length && To != From)
                    {
                        break;
                    }
                    else if (attempt == 2)
                    {
                        Console.WriteLine("To many attempts!");
                        return -2;
                    }
                }
                for (int attempt = 0; attempt <= 2; attempt++)
                {
                    try
                    {
                        Console.Write("\nAmount: ");
                        amount = Math.Round(double.Parse(Console.ReadLine()), 2);
                    }
                    catch
                    {
                        Console.WriteLine("Invalid Amount!");
                    }

                    switch (From)
                    {
                        case 1:
                            if (amount < MoneyAccount[0] && amount > 0)
                            {
                                break1 = 1;
                                break;
                            }
                            else
                            break;
                        case 2:
                            if (amount < MoneyAccount[1]  && amount > 0)
                            {
                                break1 = 1;
                                break;
                            }
                            break;
                        case 3:
                            if ( amount < MoneyAccount[2] && amount > 0)
                            {
                                break1 = 1;
                                break;
                            }
                            break;
                    }
                    if (break1 == 1)
                    {
                        break;
                    }
                    else if (attempt == 2)
                    {
                        Console.WriteLine("To many attempts!");
                        return -2;
                    }

                }
                
                StringBuilder user0 = new StringBuilder("You Will Transfer ");
                user0.AppendFormat("{0:C}", amount);
                user0.AppendFormat(" To Account {0}", To);
                Console.WriteLine(user0.ToString());

                int pin = 0;
                for (int attempt = 0; attempt <= 2; attempt++)
                {
                    try
                    {
                        Console.WriteLine("Confirm By Entering Your Pin-Code:");
                        pin = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Invalid Format!");
                    }

                    switch (user)
                    {
                        case 0:
                            if (pin == 2124) { Console.Clear(); Console.WriteLine("Success!"); break1 = 2; break; }
                            break;
                        case 1:
                            if (pin == 4452) { Console.Clear(); Console.WriteLine("Success!"); break1 = 2; break; }
                            break1 = 2;
                            break;
                        case 2:
                            if (pin == 9832) { Console.Clear(); Console.WriteLine("Success!"); break1 = 2; break; }
                            break1 = 2;
                            break;
                        case 3:
                            if (pin == 2532) { Console.Clear(); Console.WriteLine("Success!"); break1 = 2; break; }
                            break1 = 2;
                            break;
                        case 4: 
                            if (pin == 2435) { Console.Clear(); Console.WriteLine("Success!"); break1 = 2; break; }
                            break1 = 2;
                            break;
                    }
                    if (break1 == 2)
                    {
                        break;
                    }
                    else if (attempt == 2)
                    {
                        Console.WriteLine("To many attempts!");
                        return -2;
                    }
                }
                MoneyAccount[From - 1] = MoneyAccount[From - 1] - amount;
                Console.WriteLine(MoneyAccount[From - 1]);
                MoneyAccount[To - 1] = MoneyAccount[To - 1] + amount;
                Console.WriteLine(MoneyAccount[To - 1]);

                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -2; // Anny extra exception is captured here.
            }
        }
    }
}
