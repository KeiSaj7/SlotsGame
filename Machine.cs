using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SlotsGame
{
    internal class Machine : User
    {
        // Member variables
        private static Dictionary<char, int> symbols = new Dictionary<char, int>()
        {
            {'A',10},
            {'B',15},
            {'C',20},
            {'D',25},
            {'E',40}
        };
        private static Dictionary<char, int> symbols_value = new Dictionary<char, int>()
        {
            {'A',60},
            {'B',40},    
            {'C',30},
            {'D',15},
            {'E',5}
        };
        static int ROWS = 3;
        static int COLS = 3;
        private static char[,] board = new char[ROWS,COLS];
        private static int linesNum = new int();
        private static double lineBet = new int();
        // Properties
        public static int LinesNum 
        { 
            get { return linesNum; } 
        }
        // Methods
        public static void getWinnings()
        {   
            double winnings = 0;
            // rows   
            int end = linesNum;
            if (linesNum > 3) end = 3;
            for (int i = 0; i < end; i++)
            {
                char currentSymbol = board[i,0];
                for (int j = 0; j < board.GetLength(1); j++)
                {


                    if ( !(board[i,j] == currentSymbol) )
                    {
                        break;
                    }
                    else if ( j == board.GetLength(1)-1 )
                    {
                        winnings += (lineBet/linesNum) * symbols_value[currentSymbol];
                    }
                }
            }
            // cols
            Dictionary<int, int> colsLength = new Dictionary<int, int>() 
            {
                {4, 1},
                {5, 2},
                {6, 3}
            };
            if(linesNum > 3)    
            {
                int length = 0;
                if (linesNum > 6) length = 3;
                else length = colsLength[linesNum];
                for (int i = 0; i < length; i++)
                {
                    char currentSymbol = board[0, i];
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (!(board[j, i] == currentSymbol))
                        {
                            break;
                        }
                        else if (j == board.GetLength(1) - 1)
                        {
                            winnings += (lineBet/linesNum) * symbols_value[currentSymbol];
                        }
                    }
                }
            }

            // diagonals
            if (linesNum > 6)
            {
                foreach (var currentSymbol in symbols_value.Keys)
                {
                    if (board[0, 0] == currentSymbol && board[1, 1] == currentSymbol && board[2, 2] == currentSymbol)
                    {
                        winnings += (lineBet / linesNum) * symbols_value[currentSymbol];
                    }
                }
            }
            if(linesNum > 7)
            {
                foreach (var currentSymbol in symbols_value.Keys)
                {
                    if ((board[2, 0] == currentSymbol && board[1, 1] == currentSymbol && board[0, 2] == currentSymbol))
                    {
                        winnings += (lineBet / linesNum) * symbols_value[currentSymbol];
                    }
                }                   
            }
            // Substruct the bet amount from the balance if no win
            if (winnings == 0)
            {
                Balance -= lineBet;
            }
            Balance += winnings;
            Balance = Math.Round(Balance, 2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have won {0}$. Your balance is {1}$", Math.Round(winnings,2), Balance);
        }

        public static void MachineSpin()
        {
            string? PlayAgain = "";
            Machine.getBet();
            while (PlayAgain == "")
            {
                Machine.BalanceCheck();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press Enter to spin either quit to leave the casino.");
                Console.ForegroundColor= ConsoleColor.Cyan;
                PlayAgain = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Thread.Sleep(150);
                Console.Clear();
                if ( !(PlayAgain == ""))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Thanks for playing.");
                    Environment.Exit(0);
                }
                // Add elements from dictionary to a new list
                List<char> list = new List<char>();
                foreach (var symbol in symbols.Keys)
                {
                    for (int i = 0; i < symbols[symbol]; ++i)
                    {
                        list.Add(symbol);
                    }
                }
                // Fill the gameboard with random symbols from the list
                Random rng = new Random();
                int index = rng.Next(0, list.Count);
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        index = rng.Next(0, list.Count);
                        board[i, j] = list[index];
                        list.RemoveAt(index);
                    }
                }
                Machine.Display();
                Machine.getWinnings();
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Thanks for playing.");
            Environment.Exit(0);
        }

        public static void getBet()
        {
            // Get lines number and check the input
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("On how many lines you want to bet (1-7)?");
            Console.ForegroundColor = ConsoleColor.Green;
            string? input = Console.ReadLine();
            if (input == "quit")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Thanks for playing.");
                Environment.Exit(0);
            }
            bool linesCheck = int.TryParse( input, out linesNum);
            Console.WriteLine();
            while (linesCheck == false || linesNum < 1 || linesNum > 7)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number of lines! Try again please.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("On how many lines you want to bet?");
                Console.ForegroundColor = ConsoleColor.Green;
                linesCheck = int.TryParse(Console.ReadLine(), out linesNum);
                Console.WriteLine();
            }
            // Get line bet and check the input
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("How much do you want to bet?(min. bet is 1$)");
            Console.ForegroundColor = ConsoleColor.Green;
            input = Console.ReadLine();
            if (input == "quit")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Thanks for playing.");
                Environment.Exit(0);
            }
            bool lineBetCheck = double.TryParse(input, out lineBet);
            Console.WriteLine();    
            while (lineBetCheck == false || lineBet < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid amount! Try again please.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("How much do you want to bet on each line?");
                Console.ForegroundColor = ConsoleColor.Green;
                lineBetCheck = double.TryParse(Console.ReadLine(), out lineBet);
                Console.WriteLine();

            }
            // Check if player has enough money to bet
            if(lineBet > Balance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You don't have enought money to bet.");
                getBet();
            }           
        }
      
        public static void BalanceCheck()
        {
            if (lineBet > Balance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You don't have enought money to bet.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press enter to deposit either quit to leave the casino.");
                string? depositCheck = new string(string.Empty);
                depositCheck = Console.ReadLine();
                if(depositCheck == "")
                {
                    User.Deposit();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Thanks for playing.");
                    Environment.Exit(0);
                }
            }
        }
        public static void Display()
        {
            Console.Write(" - - - - - - ");
            for (int i = 0; i < ROWS; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < COLS; j++)
                {
                    if (j == 0)
                    {
                        Console.Write("| {0} | ", board[i, j]);
                    }
                    else
                    {
                        Console.Write("{0} | ", board[i, j]);
                    }                
                }
            }
            Console.WriteLine();
            Console.WriteLine(" - - - - - - ");
        }
    }
}
