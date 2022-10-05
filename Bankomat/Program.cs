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
            int user = 0; 
            for (int stage = 0; stage <= 5; stage++) // Tracks what stage of the bank we are on threw the variable stage.
            {
                if (program == true && user != -1) // Checks so the user is allowed to be in the bank.
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
                            menue();
                            break;

                          
                    }
                }
                else // Throws the user out of the bank.
                {
                    break;
                }
            }
        }
        public static bool startprogram(bool program) // Can maybe remove this whole startprogram method, but will look into that later
        {
            program = (program == true) ? true : false;
            return program;
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
                    var name = Console.ReadLine();
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
                        return -1;
                    }
                }
                return -1; // Anny extra exception is captured here.
            }
            catch (Exception ex) // Anny extra exception is captured here.
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        public static void menue()
        {
            string[] menue = { "\n1. See your accounts and balance", "2. Transfer between accounts", "3. Withdraw money", "4. Log out" };
            foreach(string list in menue) Console.WriteLine(list);
        }
    }
}
