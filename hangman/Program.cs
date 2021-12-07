using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace hangman
{
    class Program
    {
        public static List<string> letters = new List<string>();
        static void Main(string[] args)
        {
            Console.Clear();
            //todo: optionally select random word from text file for single player
            Console.WriteLine("Hangman");
            draw(12, "", false);
            Console.WriteLine("Press the any key to start");
            Console.ReadKey();
            while (true) //until user exits
            {
                Console.Clear();
                string word = ask();
                int turns = askturns();
                int invturns = 11 - turns;
                string winning = "";
                bool won = false;
                bool first = true;
                while (won == false)
                {
                    draw(invturns, word);
                    if (first)
                    {
                        first = !first;
                        Console.WriteLine("Options: Letter, 'Restart', 'Exit'");
                    }
                    string guessed = input(word);
                    if(guessed == "Incorrect")
                    {
                        invturns++;
                    }
                    winning = check(word,invturns);
                    if (winning != "Null")
                    {
                        won = true;
                    }
                }
                if (winning == "Won")
                {
                    draw(invturns, word);
                    Console.WriteLine("You won!");
                }
                else if (winning == "Lost")
                {
                    draw(invturns, word);
                    Console.WriteLine("You Lost!");
                    Console.WriteLine("The word was: " + word);
                }
                askexit();
            }
        }
        public static string check(string word, int invturns)
        {
            if (invturns == 11)
            {
                return "Lost"; // Lost
            }
            int unguessed = 0;
            word = word.ToLower();
            foreach (char c in word)
            {
                if (c != ' ')
                {
                    if (letters.Contains(c.ToString()) == false)
                    {
                        unguessed++;
                    }
                }
            }
            if (unguessed == 0)
            {
                return "Won";
            }
            return "Null";
        }
        public static string input(string word) 
        {
            bool guessing = true;
            string guess = "";
            while (guessing)
            {
                try
                {
                    Console.Write("Guess: ");
                    guess = Console.ReadLine();
                    guess = Regex.Replace(guess, "[^A-Za-z ]", "");
                    guess = guess.ToLower();
                    if (guess.Length == 1 && guess != " " && guess != "")
                    {
                        if (letters.Contains(guess))
                        {
                            Console.WriteLine("Already Guessed");
                        }
                        else
                        {
                            guessing = false;
                        }
                    }
                    else
                    {
                        askexit(guess);
                        Console.WriteLine("Invalid Input");
                    }

                }
                catch
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            letters.Add(guess);
            string wordlc = word.ToLower();
            if (wordlc.Contains(guess))
            {
                return "Correct"; //Correct
            }
            else
            {
                return "Incorrect"; //Incorrect
            }
        }
        static void draw(int gallowsint, string word, bool clear = true)
        {
            if (clear == true)
            {
                Console.Clear();
            }
            foreach (char c in word)
            {
                if (c != ' ')
                {
                    if (letters.Contains(c.ToString().ToLower()) || letters.Contains(c.ToString().ToUpper()))
                    {
                        Console.Write(c);
                    }
                    else
                    {
                        Console.Write("_");
                    }
                }
                else
                {
                    Console.Write(" ");
                }

            }
            Console.WriteLine();
            for (int i = 0; i < 6; i++)
            {
                for(int i2 = 0; i2 < 12; i2++)
                {
                    Console.Write(gallows[gallowsint,i,i2]);
                }
                Console.WriteLine();
            }
            letters.ForEach(delegate (string letter)
            {
                if (word.ToLower().Contains(letter.ToLower()) == false)
                {
                    Console.Write(letter.ToUpper());
                }
            });
            Console.WriteLine();
        }
        public static int askturns()
        {
            int turns = 0;
            bool done = false;
            while (done == false)
            {
                Console.Clear();
                Console.Write("How many turns (6 or 11): ");
                string turninput = Console.ReadLine();
                try
                {
                    turns = Int32.Parse(turninput);
                    if (turns == 6 || turns == 11)
                    {
                        done = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            return turns;
        }
        public static void askexit(string command = "")
        {
            string row = "";
            if (command == "")
            {
                Console.WriteLine("Restart or exit: ");
                row = Console.ReadLine();
                row = row.ToLower();
            }
            else
            {
                row = command;
            }
            if (row == "exit")
            {
                Environment.Exit(0);
            }
            else if (row == "restart")
            {
                // Starts a new instance of the program itself
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                // Closes the current process
                Environment.Exit(0);
            }
        }
        public static string ask()
        {
            Console.Clear();
            bool choosing = true;
            while (choosing)
            {
                try
                {
                    Console.Write("How many players (1 or 2): ");
                    string choice = Console.ReadLine();
                    if (choice == "2")
                    {
                        choosing = false;
                    }
                    else if (choice == "1")
                    {
                        try
                        {
                            //todo: querry random.org for quantum random numbers
                            Random rnd = new Random();
                            string[] lines = System.IO.File.ReadAllLines("words.txt");
                            return lines[rnd.Next(0,lines.Length)];
                        }
                        catch
                        {
                            Console.WriteLine("Failed to choose random word, falling back to 2 player mode");
                            Console.WriteLine("Press the any key to continue");
                            Console.ReadKey();
                            choosing = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            while (true)
            {
                Console.Clear();
                Console.Write("Enter word: ");
                var pass = string.Empty;
                ConsoleKey key;
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        Console.Write("\b \b");
                        pass = pass[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        pass += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
                //@"[\p{L} ]+$" "^[A - Za - z] +$" //https://stackoverflow.com/questions/2950989/net-regex-for-letters-and-spaces/2950998
                pass = Regex.Replace(pass, "[^A-Za-z ]", "");
                if (string.IsNullOrWhiteSpace(pass) == false)
                {
                    return pass;
                }
            }

        }
        public static string[,,] gallows = new String[13, 6, 12] {
{
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "}
},
        {
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {"_", "_", "_", "_", "_", "_", "_", " ", " ", " ", " ", " "}
        },

        {
                {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {"_", "_", "_", "|", "_", "_", "_", " ", " ", " ", " ", " "}
        },

        {
                {" ", " ", " ", "_", "_", "_", "_", "_", "_", "_", "_", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {"_", "_", "_", "|", "_", "_", "_", " ", " ", " ", " ", " "}
        },

        {
                {" ", " ", " ", "_", "_", "_", "_", "_", "_", "_", "_", " "},
                {" ", " ", " ", "|", "/", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {"_", "_", "_", "|", "_", "_", "_", " ", " ", " ", " ", " "}
        },

        {
                {" ", " ", " ", "_", "_", "_", "_", "_", "_", "_", "_", " "},
                {" ", " ", " ", "|", "/", " ", " ", " ", " ", " ", "|", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {"_", "_", "_", "|", "_", "_", "_", " ", " ", " ", " ", " "}
       },
         {
                {" ", " ", " ", "_", "_", "_", "_", "_", "_", "_", "_", " "},
                {" ", " ", " ", "|", "/", " ", " ", " ", " ", " ", "|", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", "O", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {"_", "_", "_", "|", "_", "_", "_", " ", " ", " ", " ", " "}
       },
         {
                {" ", " ", " ", "_", "_", "_", "_", "_", "_", "_", "_", " "},
                {" ", " ", " ", "|", "/", " ", " ", " ", " ", " ", "|", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", "O", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", "|", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {"_", "_", "_", "|", "_", "_", "_", " ", " ", " ", " ", " "}
       },
              {
                {" ", " ", " ", "_", "_", "_", "_", "_", "_", "_", "_", " "},
                {" ", " ", " ", "|", "/", " ", " ", " ", " ", " ", "|", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", "O", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", "/", "|", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {"_", "_", "_", "|", "_", "_", "_", " ", " ", " ", " ", " "}
       },
              {
                {" ", " ", " ", "_", "_", "_", "_", "_", "_", "_", "_", " "},
                {" ", " ", " ", "|", "/", " ", " ", " ", " ", " ", "|", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", "O", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", "/", "|", "\\"},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {"_", "_", "_", "|", "_", "_", "_", " ", " ", " ", " ", " "}
       },
                 {
                {" ", " ", " ", "_", "_", "_", "_", "_", "_", "_", "_", " "},
                {" ", " ", " ", "|", "/", " ", " ", " ", " ", " ", "|", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", "O", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", "/", "|", "\\"},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", "/", " ", " "},
                {"_", "_", "_", "|", "_", "_", "_", " ", " ", " ", " ", " "}
       },
       {
                {" ", " ", " ", "_", "_", "_", "_", "_", "_", "_", "_", " "},
                {" ", " ", " ", "|", "/", " ", " ", " ", " ", " ", "|", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", "O", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", "/", "|", "\\"},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", "/", " ", "\\"},
                {"_", "_", "_", "|", "_", "_", "_", " ", " ", " ", " ", " "}
       },
                 {                   
                {" ", " ", " ", "_", "_", "_", "_", "_", "_", "_", "_", " "},
                {" ", " ", " ", "|", "/", "M", "a", "d", "e", " ", "|", " "},
                {" ", " ", " ", "|", "B", "y", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", "|", "D", "e", "n", "n", "e", "t", "h", " "},
                {" ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", " "},
                {"_", "_", "_", "|", "_", "_", "_", " ", "V", "1", ".", "1"}
       }};
    }
}
