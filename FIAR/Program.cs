using System;

namespace FIAR
{
    class Program
    {
        static void Main(string[] args)
        {
            //todo: AI
            string[,] positions = new String[6, 7]
            {
                {" ", " ", " ", " ", " ", " ", " "},
                {"M", "a", "d", "e", " ", "b", "y"},
                {" ", " ", " ", " ", " ", " ", " "},
                {"D", "e", "n", "n", "e", "t", "h"},
                {" ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " "}
            };
            draw(positions);
            Console.WriteLine(check(positions)) ;
            Console.WriteLine("");
            Console.WriteLine("Version 1.0");
            Console.WriteLine("Press the any key to start");
            Console.ReadKey();
            positions = new String[6, 7]
{
                {" ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " "},
                {" ", " ", " ", " ", " ", " ", " "}
            };
            string playerchar = "O";
            draw(positions);
            Console.WriteLine("Options: row, 'exit' and 'restart'");
            bool won = false;
            while (won == false)
            {
                positions = ask(positions, playerchar, won);
                draw(positions);
                string checkstring = check(positions);
                if (checkstring == "O")
                {
                    won = true;
                    Console.WriteLine("O won!");
                }
                else if (checkstring == "@")
                {
                    won = true;
                    Console.WriteLine("@ won!");
                }
                else if (checkstring == "F")
                {
                    won = true;
                    Console.WriteLine("Stalemate!");
                }
                if (playerchar == "O")
                { playerchar = "@"; }
                else { playerchar = "O"; }
            }
            while (true)
            {
                positions = ask(positions, playerchar, won);
            }
        }
        static string[,] fall(string[,] positions, string playerchar, int row)
        {
            bool falling = true;
            int i = 1;
            while (falling == true)
            {
                
                if (positions[i, row] == " " && i < 5)
                {
                    positions[i - 1, row] = " ";
                    positions[i, row] = playerchar;
                    i++;
                }
                else
                {
                    falling = false;
                }
            }
            return positions;
        }
        static string[,] ask(string[,] positions, string playerchar, bool won)
        {
            bool done = false;
            while (done == false)
            {
                if (won == false)
                {
                    Console.WriteLine("Player " + playerchar + "'s turn");
                    Console.Write("Enter Row: ");
                }
                else
                {
                    Console.Write("Restart or Exit?: ");
                }
                string row = Console.ReadLine();
                row = row.ToLower();
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
                try
                {
                    int rowint = Int32.Parse(row.ToString()) - 1;
                    if (positions[0, rowint] == " " && won == false)
                    {
                        positions[0, rowint] = playerchar;
                        fall(positions, playerchar, rowint);
                        done = true;
                    }
                    else { Console.WriteLine("Invalid Input, please try again"); }
                }
                catch { Console.WriteLine("Invalid Input, please try again"); };
            }

            return positions;
        }
        static void draw(string[,] positions)
        {
            Console.Clear();
            Console.WriteLine("Four in a row");
            Console.WriteLine("");
            Console.WriteLine(" 1   2   3   4   5   6   7");
            for (int i2 = 0; i2 < 5; i2++) {
                Console.Write(" ");
                for (int i = 0; i < 6; i++)
                {
                    Console.Write(positions[i2, i]);
                    Console.Write(" | ");
                }
                Console.WriteLine(positions[i2, 6]);
                Console.WriteLine(" = = = = = = = = = = = = =");
            }
        }
        static string check(string[,] positions)
        {
            string[] fours = new string[25];
            string tstring = "";
            int counter = 0;
            for (int i2 = 0; i2 < 7; i2++)
            {
                for (int i = 0; i < 6; i++)
                {
                    tstring = tstring + positions[i, i2];
                }
                fours[counter] = tstring;
                counter++;
                tstring = "";
            }
            for (int i2 = 0; i2 < 6; i2++)
            {
                for (int i = 0; i < 7; i++)
                {
                    tstring = tstring + positions[i2, i];
                }
                fours[counter] = tstring;
                counter++;
                tstring = "";
            }

            void diagonals(string[,] positionsdiag)
            {
                int x = 3;
                int y = 0;
                for (int i = 0; i < 4; i++)
                {
                    tstring = tstring + positionsdiag[x, y];
                    x = x - 1;
                    y++;
                }
                fours[counter] = tstring;
                counter++;
                tstring = "";

                for (int i2 = 0; i2 < 3; i2++)
                {
                    x = 4;
                    y = 0 + i2;
                    for (int i = 0; i < 5; i++)
                    {
                        tstring = tstring + positionsdiag[x, y];
                        x = x - 1;
                        y++;
                    }
                    fours[counter] = tstring;
                    counter++;
                    tstring = "";
                }
                x = 4;
                y = 3;
                for (int i = 0; i < 4; i++)
                {
                    tstring = tstring + positionsdiag[x, y];
                    x = x - 1;
                    y++;
                }
                fours[counter] = tstring;
                counter++;
                tstring = "";
            }
            diagonals(positions);
            string[,] positions2 = (string[,])positions.Clone();
            Reverse2DimArray(positions2);
            diagonals(positions2);
            int nullcount = 0;
            for (int i = 0; i < counter; i++)
            {
                if (fours[i].Contains("OOOO"))
                {
                    return "O";
                }
                else if (fours[i].Contains("@@@@"))
                {
                    return "@";
                }
                if (fours[i].Contains(" "))
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
        public static void Reverse2DimArray(string[,] theArray)
        {
            for (int rowIndex = 0;
                 rowIndex <= (theArray.GetUpperBound(0)); rowIndex++)
            {
                for (int colIndex = 0;
                     colIndex <= (theArray.GetUpperBound(1) / 2); colIndex++)
                {
                    string tempHolder = theArray[rowIndex, colIndex];
                    theArray[rowIndex, colIndex] =
                      theArray[rowIndex, theArray.GetUpperBound(1) - colIndex];
                    theArray[rowIndex, theArray.GetUpperBound(1) - colIndex] =
                      tempHolder;
                }
            }
        }
    }
}
