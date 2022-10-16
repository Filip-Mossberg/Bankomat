using System;
using System.Text;

namespace Bankomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int user = 0;
 
            // Jagged Array with all the user money accounts. 
            double[][] MoneyAccount = new double[5][];
            MoneyAccount[0] = new double[3] { 403.25, 23400.83, 102420.63 };
            MoneyAccount[1] = new double[2] { 1394.34, 43742.04 };
            MoneyAccount[2] = new double[1] { 3493.52 };
            MoneyAccount[3] = new double[3] { 1904.78, 18304.94, 74323.20 };
            MoneyAccount[4] = new double[2] { 1452.86, 173744.14 };

            // Array that saves user input that is being used in different methods.
            string[] PickSaver = { "", "", "" };

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
                            int pick = Menu(user);
                            switch (pick)
                            {
                                case 1:
                                    AccountCheck(MoneyAccount[user]);
                                    stage = Return();
                                    break;
                                case 2:
                                    AccountCheck(MoneyAccount[user]);
                                    (stage, PickSaver[0]) = From(MoneyAccount[user], 1);
                                    if (stage == -2) { break; }
                                    (stage, PickSaver[1]) = To(MoneyAccount[user], PickSaver);
                                    if (stage == -2) { break; }
                                    (stage, PickSaver[2]) = Amount(MoneyAccount[user], PickSaver, 1);
                                    if (stage == -2) { break; }
                                    Message(MoneyAccount[user], PickSaver, 1);
                                    (stage) = PinCode(user);
                                    if (stage == -2) { break; }
                                    UpdateValues(MoneyAccount[user], PickSaver, 1);
                                    AccountCheck(MoneyAccount[user]); stage = Return(); 
                                    break;
                                case 3:
                                    AccountCheck(MoneyAccount[user]);
                                    (stage, PickSaver[0]) = From(MoneyAccount[user], 2);
                                    if (stage == -2) { break; }
                                    (stage, PickSaver[2]) = Amount(MoneyAccount[user], PickSaver, 2);
                                    if (stage == -2) { break; }
                                    Message(MoneyAccount[user], PickSaver, 2);
                                    (stage) = PinCode(user);
                                    if (stage == -2) { break; }
                                    UpdateValues(MoneyAccount[user], PickSaver, 2);
                                    AccountCheck(MoneyAccount[user]); stage = Return();
                                    break;
                                case 4:
                                    Console.Clear();
                                    stage = -1;
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
            String[] Welcome = { "Välkommen till banken!\n", "Välkommen till din affärsbank!\n", "Välkommen till din lokala bank!\n" };
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
                    Console.Write("Ange användarnamn:");
                    var name = Console.ReadLine().ToLower();
                    Console.Write("Ange din pinkod:");
                    var pincode = Console.ReadLine();

                    for (count = 0; count <= 4; count++) // Loop that goes threw all the accounts in the account array to try and find a match with the user input.
                    {
                        check = (name == accounts[count, 0] && pincode == accounts[count, 1]) ? true : false;
                        if (check == true) 
                        {
                            int user = count;
                            return count;
                        }
                        else if (count == 4 && check == false)
                        {
                            Console.Clear();
                            Console.WriteLine("Fel användarnamn eller pinkod!\n");
                            
                        }
                    }
                    if (attempt == 2 && check == false) // If the user has failed to login on 3 attempts then a value of -1 is returned. 
                    {
                        Console.WriteLine("För många försök!");
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
        public static int Menu(int user) // Lists the menu and lets the user pick a option from the menue.
        {
            for (int WelcomeMessage = 0; WelcomeMessage < 1; WelcomeMessage++)
            {
                Console.Clear();
                switch (user)
                {
                    case 0:
                        Console.WriteLine("Välkommen Filip!");
                        break;
                    case 1:
                        Console.WriteLine("Välkommen Noah!");
                        break;
                    case 2:
                        Console.WriteLine("Välkommen Bella!");
                        break;
                    case 3:
                        Console.WriteLine("Välkommen Joakim!");
                        break;
                    case 4:
                        Console.WriteLine("Välkommen Ulrika!");
                        break;
                }
            }
            string[] menue = { "\n1. Se dina konton och saldo", "2. Överföring mellan konto", "3. Ta ut pengar", "4. Logga ut\n" };
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
                            Console.WriteLine("Ogilltigt val!\n");
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
                    Console.WriteLine("För många försök!");
                    return -2; 
                }
            }
            return -2; // Anny extra exception is captured here.

        }
        private static void AccountCheck(double[] MoneyAccount) // Writes out a specific users accounts depending on how many accounts they have.
        {
            Console.Clear();
            if (MoneyAccount.Length == 1) // If the user has 1 account.
            {
                StringBuilder user0 = new StringBuilder("[1] Lönekonto: ");
                user0.AppendFormat("{0:C}", MoneyAccount[0]);
                Console.WriteLine(user0.ToString());
            }
            if (MoneyAccount.Length == 2) // If the user has 2 accounts.
            {
                StringBuilder user0 = new StringBuilder("[1] Lönekonto: ");
                user0.AppendFormat("{0:C}", MoneyAccount[0]);
                Console.WriteLine(user0.ToString());
                StringBuilder user00 = new StringBuilder("[2] Investeringssparkonto: ");
                user00.AppendFormat("{0:C}", MoneyAccount[1]);
                Console.WriteLine(user00.ToString());
            }
            if (MoneyAccount.Length == 3) // If the user has 3 accounts.
            {
                StringBuilder user0 = new StringBuilder("[1] Lönekonto: ");
                user0.AppendFormat("{0:C}", MoneyAccount[0]);
                Console.WriteLine(user0.ToString());
                StringBuilder user00 = new StringBuilder("[2] Investeringssparkonto: ");
                user00.AppendFormat("{0:C}", MoneyAccount[1]);
                Console.WriteLine(user00.ToString());
                StringBuilder user000 = new StringBuilder("[3] Sparkonto: ");
                user000.AppendFormat("{0:C}", MoneyAccount[2]);
                Console.WriteLine(user000.ToString());
            }

        }
        public static int Return()
        {
            Console.WriteLine("\nKlicka enter för att komma till huvudmenyn:");
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
                        Console.WriteLine("Ogilltigt val!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -2; // Anny extra exception is captured here.
                }
                if (attempt == 2)
                {
                    Console.WriteLine("För många försök!");
                    return -2;
                }
            }
            return -2; // Anny extra exception is captured here.
        }
        public static (int, string) From(double[] MoneyAccount, int Decider) // Takes the user input on witch account they want to Transfer or Withdraw from.
        {
            int From1 = 0;
            int error = 0;
            int error2 = 0;
            string[] PickSaver = { "" };

            for (int attempt = 0; attempt <= 2; attempt++)
            {
                try
                {
                    if (Decider == 1) { Console.Write("\nÖverför från:"); }
                    else { Console.Write("\nTa ut från:"); }
                    From1 = int.Parse(Console.ReadLine());
                    error2 = 0;
                    if (error == 3)
                    {
                        return (-2, PickSaver[0]);
                    }
                }
                catch
                {
                    if (attempt != 2)
                    {
                        Console.WriteLine("Ogilltig summa!");
                        error++;
                        error2++;
                    }
                }
                if (From1 > 0 && From1 <= MoneyAccount.Length)
                {
                    PickSaver[0] = From1.ToString();
                    return (0, PickSaver[0]);
                }
                else if (attempt == 2)
                {
                    Console.WriteLine("För många försök!");
                    return (-2, PickSaver[0]);
                }
                else
                {
                    if (error2 < 1)
                    {
                        Console.WriteLine("Ogilltigt konto!");
                    }
                }
            }
            return (-2, PickSaver[0]); // Anny extra exception is captured here.
        }
        public static (int, string) To(double[] MoneyAccount, string[] PickSaver) // Takes the user input on witch account they want to Transfer to.
        {
            int From = int.Parse(PickSaver[0]);
            int To = 0;
            int error = 0;
            int error2 = 0;

            for (int attempt = 0; attempt <= 2; attempt++)
            {
                try
                {
                    Console.Write("\nÖverför till:");
                    To = int.Parse(Console.ReadLine());
                    error2 = 0;
                    if (error == 3)
                    {
                        return (-2, PickSaver[1]);
                    }
                }
                catch
                {
                    if (attempt != 2)
                    {
                        Console.WriteLine("Ogilltigt konto!");
                        error++;
                        error2++;
                    }
                }
                if (To > 0 && To <= MoneyAccount.Length && To != From)
                {
                    PickSaver[1] = To.ToString();
                    return (0, PickSaver[1]);
                }
                else if (attempt == 2)
                {
                    Console.WriteLine("För många försök!");
                    return (-2, PickSaver[1]);
                }
                else
                {
                    if (error2 < 1)
                    {
                        Console.WriteLine("Ogilltigt konto!");
                    }
                }
            } 
            return (-2, PickSaver[1]); // Anny extra exception is captured here.
        }
        public static (int, string) Amount(double[] MoneyAccount, string[] PickSaver, int Decider) // Takes the user input on the amount that is getting Transfered or Withdrawn.
        {
            int From = int.Parse(PickSaver[0]);
            if (Decider == 1) { int To = int.Parse(PickSaver[1]); }
            double amount = Math.Round(0.00, 2);
            int error = 0;
            int error2 = 0;

            for (int attempt = 0; attempt <= 2; attempt++)
            {
                try
                {
                    Console.Write("\nAmount: ");
                    amount = Math.Round(double.Parse(Console.ReadLine()), 2);
                    error2 = 0;
                    if (error == 3)
                    {
                        return (-2, PickSaver[2]);
                    }
                }
                catch
                {
                    if (attempt != 2)
                    {
                        Console.WriteLine("Ogilltig summa!");
                        error++;
                        error2++;
                    }

                }
                switch (From)
                {
                    case 1:
                        if (amount < MoneyAccount[0] && amount > 0)
                        {
                            PickSaver[2] = amount.ToString();
                            return (2, PickSaver[2]);
                        }
                        break;
                    case 2:
                        if (amount < MoneyAccount[1] && amount > 0)
                        {
                            PickSaver[2] = amount.ToString();
                            return (2, PickSaver[2]);
                        }
                        break;
                    case 3:
                        if (amount < MoneyAccount[2] && amount > 0)
                        {
                            PickSaver[2] = amount.ToString();
                            return (2, PickSaver[2]);
                        }
                        break;
                }
                if (error2 < 1)
                {
                    Console.WriteLine("Ogilltig summa!");
                }
                else if (attempt == 2)
                {
                    Console.WriteLine("För många försök!");
                    return (-2, PickSaver[2]);
                }
            }
            return (-2, PickSaver[2]); // Anny extra exception is captured here.
        }

        public static void Message(double[] MoneyAccount, string[] PickSaver, int Decider) // Writes a message that tells the user what is about to happen based on the users earlier inputs. 
        {
            int From = int.Parse(PickSaver[0]);
            double amount = double.Parse(PickSaver[2]);

            if (Decider == 1)
            {
                int To = int.Parse(PickSaver[1]);
                StringBuilder user0 = new StringBuilder("Du kommer överföra ");
                user0.AppendFormat("{0:C}", amount);
                user0.AppendFormat(" till {0}", To);
                Console.WriteLine(user0.ToString());
            }
            else
            {
                StringBuilder user0 = new StringBuilder("Du kommer ta ut ");
                user0.AppendFormat("{0:C}", amount);
                user0.AppendFormat(" från {0}", From);
                Console.WriteLine(user0.ToString());
            }
        }
        public static int PinCode(int user) // The user needs to enter their pincode to confirm the change. 
        {
            for (int attempt = 0; attempt <= 2; attempt++)
            {
                int pin = 0;
                int error = 0;
                int error2 = 0;

                try
                {
                    Console.WriteLine("Bekräfta genom att ange din pinkod:");
                    pin = int.Parse(Console.ReadLine());
                    error2 = 0;
                    if (error == 3)
                    {
                        return -2;
                    }
                }
                catch
                {
                    if (attempt != 2)
                    {
                        Console.WriteLine("Ogilltigt format!");
                        error++;
                        error2++;
                    }
                }

                switch (user)
                {
                    case 0:
                        if (pin == 2124) { Console.Clear(); Console.WriteLine("Lyckad ändring!"); return 2; }
                        break;
                    case 1:
                        if (pin == 4452) { Console.Clear(); Console.WriteLine("Lyckad ändring!"); return 2; }
                        break;
                    case 2:
                        if (pin == 9832) { Console.Clear(); Console.WriteLine("Lyckad ändring!"); return 2; }
                        break;
                    case 3:
                        if (pin == 2532) { Console.Clear(); Console.WriteLine("Lyckad ändring!"); return 2; }
                        break;
                    case 4:
                        if (pin == 2435) { Console.Clear(); Console.WriteLine("Lyckad ändring!"); return 2; }
                        break;
                }
                if (error2 < 1)
                {
                    Console.WriteLine("Ogilltig pinkod!");
                }
                else if (attempt == 2)
                {
                    Console.WriteLine("För många försök!");
                    return -2;
                }
            }
            return -2;
        }
        public static void UpdateValues(double[] MoneyAccount, string[] PickSaver, int Decider) // Updates the values, Decider == 1 is if you Transfered and Decider == 2 is if you Withdrawed.
        {
            int From = int.Parse(PickSaver[0]);
            double amount = double.Parse(PickSaver[2]);

            if (Decider == 1)
            {
                int To = int.Parse(PickSaver[1]);
                MoneyAccount[From - 1] = MoneyAccount[From - 1] - amount;
                Console.WriteLine(MoneyAccount[From - 1]);
                MoneyAccount[To - 1] = MoneyAccount[To - 1] + amount;
                Console.WriteLine(MoneyAccount[To - 1]);
            }
            else
            {
                MoneyAccount[From - 1] = MoneyAccount[From - 1] - amount;
                Console.WriteLine(MoneyAccount[From - 1]);
            }
        }
    }
}
