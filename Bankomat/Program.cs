using System;

namespace Bankomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Welcome();
        }
        public static void Welcome()
        {
            String[] Welcome = { "Welcome to the bank!", "Welcome to your commercial bank!", "Welcome to your local bank!" };
            Random message = new Random();
            int number = message.Next(0, 4);
            Console.WriteLine(Welcome[number]);
        }
    }
}
