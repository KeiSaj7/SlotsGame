using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotsGame
{
    internal class User
    {
        // Member variables
        private static string nickname = new string(string.Empty);
        private static double balance = new double();
        // Properties
        public static double Balance 
        {
            get { return balance; }
            set { balance = value; }
        }
        public static string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }

        // Methods
        public static void Deposit()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("How much you want to deposit?");
            Console.ForegroundColor = ConsoleColor.Green;
            string? value = Console.ReadLine();
            Console.WriteLine();
            if(value == "quit")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Thanks for playing.");
                Environment.Exit(0);
            }
            double amount = new double();
            bool doubleCheck = double.TryParse(value, out amount);
            if (doubleCheck && amount > 0)
            {
                Balance += amount;
                Console.WriteLine("Your balance is {0}$", Balance);
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid amount! Try again please.");
                Deposit();
            }
        }
        public static void Introduce()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;;
            Console.WriteLine("Welcome to our casino.\nWhenever you wish to quit, type 'quit'.\nHave fun!");
            Console.WriteLine();
            User.Deposit();
        }




    }
}
