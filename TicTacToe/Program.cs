using System;

namespace TicTacToe
{
    class Program
    {

        static void Main(string[] args)
        {
			//todo: AI
            string[,] positions = new String[3, 3]
            {
                {"B", "y", "D"},
                {"e", "n", "n"},
                {"e", "t", "h"}
            };
            draw(positions);
            Console.WriteLine("Version 1.0");
            Console.WriteLine("Press the any key to start");
            Console.ReadKey();
            positions = new String[3, 3]
            {
                {" ", " ", " "},
                {" ", " ", " "},
                {" ", " ", " "}
            };
            string playerchar = "X";
            draw(positions);
            Console.WriteLine("Options: 'x,y', 'exit' and 'restart'");
            bool won = false;
            while (won == false)
            {
                positions = ask(positions, playerchar, won);
                draw(positions);
                string checkstring = check(positions);
                if (checkstring == "X")
                {
                    won = true;
                    Console.WriteLine("X won!");
                }
                else if (checkstring == "O")
                {
                    won = true;
                    Console.WriteLine("O won!");
                }
                else if (checkstring == "F")
                {
                    won = true;
                    Console.WriteLine("Stalemate!");
                }
                if (playerchar == "X")
                { playerchar = "O"; }
                else { playerchar = "X"; }
            }
            while (true)
            {
                positions = ask(positions, playerchar, won);
            }
        }

        static void draw(string[,] positions)
        {
            Console.Clear();
            Console.WriteLine("TicTacToe");
            Console.WriteLine("");
            Console.Write(" ");
            Console.Write(positions[0, 0]);
            Console.Write(" | ");
            Console.Write(positions[0, 1]);
            Console.Write(" | ");
            Console.WriteLine(positions[0, 2]);
            Console.WriteLine(" = = = = =");
            Console.Write(" ");
            Console.Write(positions[1, 0]);
            Console.Write(" | ");
            Console.Write(positions[1, 1]);
            Console.Write(" | ");
            Console.WriteLine(positions[1, 2]);
            Console.WriteLine(" = = = = =");
            Console.Write(" ");
            Console.Write(positions[2, 0]);
            Console.Write(" | ");
            Console.Write(positions[2, 1]);
            Console.Write(" | ");
            Console.WriteLine(positions[2, 2]);
            Console.WriteLine("");
        }

        static string[,] ask(string[,] positions, string playerchar, bool won)
        {
            bool done = false;
            while (done == false) {
                if (won == false)
                {
					Console.WriteLine("Player " + playerchar + "'s turn");
                    Console.Write("Enter Coordinates (x,y): "); //todo: make a better coord system
                }
                else
                {
                    Console.Write("Restart or Exit?: ");
                }
                //todo: check validity
                string coords = Console.ReadLine();
                coords = coords.ToLower();
                if (coords == "exit")
                {
                    Environment.Exit(0);
                }
                else if (coords == "restart")
                {
                    // Starts a new instance of the program itself
                    System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                    // Closes the current process
                    Environment.Exit(0);
                }
                try {
                    string[] coordsarray = coords.Split(",");
                    int[] coordsint = new int[2];
                    coordsint[0] = Int32.Parse(coordsarray[0].ToString()) - 1;
                    coordsint[1] = Int32.Parse(coordsarray[1].ToString()) - 1;
                    //Console.WriteLine(coordsint[0].ToString()); 
                    if (positions[coordsint[0], coordsint[1]] == " " && won == false)
                    {
                        positions[coordsint[0], coordsint[1]] = playerchar;
                        done = true;
                    }
                    else { Console.WriteLine("Invalid Input, please try again"); }
                }
                catch { Console.WriteLine("Invalid Input, please try again"); };
            }
            return positions;
        }

        static string check(string[,] positions)
        {
            string[] threes = new string[8];
            string tstring = "";
            for (int i2 = 0; i2 < 3; i2++)
            {
                for (int i = 0; i < 3; i++)
                {
                    tstring = tstring + positions[i, i2];
                }
                threes[i2] = tstring;
                tstring = "";
            }
            for (int i2 = 0; i2 < 3; i2++)
            {
                for (int i = 0; i < 3; i++)
                {
                    tstring = tstring + positions[i2, i];
                }
                threes[i2 + 3] = tstring;
                tstring = "";
            }
            for (int i = 0; i < 3; i++)
            {
                tstring = tstring + positions[i, i];
            }
            threes[6] = tstring;
            tstring = "";
            for (int i = 0; i < 3; i++)
            {
                tstring = tstring + positions[2 - i, i];
            }
            threes[7] = tstring;
            int nullcount = 0;
            for(int i = 0; i < threes.Length; i++)
            {
                if (threes[i] == "XXX")
                {
                    return "X";
                }
                else if (threes[i] == "OOO")
                {
                    return "O";
                }
                if (threes[i].Contains(" "))
                {
                    nullcount++;
                }
            }
            if (nullcount == 0)
            {
                return "F"; //F for Full
            }
            else
            {
                return " ";
            }
        }
    }
}