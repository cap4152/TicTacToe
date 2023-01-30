using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace TicTacToe
{
    
    class Cell
    {
        public int Value { get; set; }
        public bool IsTaken { get; set; }
        public string IsTakenBy { get; set; }
    }
    class Program
    {
        public static Cell MyCreateTable()
        {
            Cell cell = new Cell();
            return cell;
        }

        public static void InstantiateTable(int Lines)
        {
            Cell[,] table= new Cell[Lines, Lines];

        }
        

        static void DefaultConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }
        static void ChangePlayerConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        static void ChangeAiConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        static int RandomGenerator(int NumberMin, int NumberMax)
        {
            int val;
            int min = NumberMin;
            int max = NumberMax;
            System.Threading.Thread.Sleep(1);
            Random random = new Random();
            val = random.Next(min, max);
            return val;
        }

        static void PrintTableValue(int[,] tabel, int i, int j)
        {
            int Player = 100;
            int AI = 200;
            if (tabel[i, j] == Player)
            {
                ChangePlayerConsoleColor();
                Console.Write("X");
                DefaultConsoleColor();
            }
            else if (tabel[i, j] == AI)
            {
                ChangeAiConsoleColor();
                Console.Write("0");
                DefaultConsoleColor();
            }
            else
            {
                Console.Write(tabel[i, j]);
            }
        }
        static void PrintTableBorder(int[,] tabel, int length, int maxval)
        {
            int digits = length;
            int Max = maxval;
            int j;

            for (j = 0; j < tabel.GetLength(1); j++)
            {
                Console.Write("+-");
                for (int k = 1; k < digits; k++)
                {
                    Console.Write("--");
                    if (Max < 100)
                        Console.Write("-");
                }
                Console.Write("+");
            }
            Console.WriteLine();
        }
        static void PrintTable(int[,] tabel)
        {
            Console.Clear();
            int MaxValue = tabel.GetLength(0) * tabel.GetLength(1);
            int digits = MaxValue.ToString().Length;

            int i; int j;

            for (i = 0; i < tabel.GetLength(0); i++)
            {
                PrintTableBorder(tabel, digits, MaxValue); /// top line

                for (j = 0; j < tabel.GetLength(1); j++) // mid line
                {
                    Console.Write("|");
                    for (int k = 1; k < digits; k++)
                    {
                        Console.Write(" ");
                        if (tabel[i, j] < 10 || tabel[i, j] == 100 || tabel[i, j] == 200) // add spaces for values
                            Console.Write(" ");
                    }

                    PrintTableValue(tabel, i, j);         // print table value

                    for (int k = 1; k < digits; k++)
                        Console.Write(" ");
                    Console.Write("|");
                }
                Console.WriteLine();

                PrintTableBorder(tabel, digits, MaxValue); //bot line
            }
        }

        static bool Check_win_all_casses(int[,] tabel)
        {
            // function incorporates all other checks for a winner 
            // if any of the other functions return true, this function returns true

            if (Check_win_diag(tabel) || Check_win_row(tabel) || Check_win_coll(tabel))
                return true;
            else
                return false;
        }
        static bool Check_win_row(int[,] tabel)
        {
            int firstElement = tabel[0, 0];
            int j = 0;
            int i = 0;
            bool check = false;

            for (i = 0; i < tabel.GetLength(0); i++)
            {
                firstElement = tabel[i, 0];
                j = 0;
                while (j < tabel.GetLength(1))
                {
                    if (firstElement != tabel[i, j])
                        break;
                    if (j == tabel.GetLength(1) - 1)
                        check = true;
                    j++;
                }
                if (check == true)
                    break;
            }
            return check;
        }
        static bool Check_win_coll(int[,] tabel)
        {
            int firstElement = tabel[0, 0];
            int j = 0;
            int i = 0;
            bool check = false;

            for (j = 0; j < tabel.GetLength(0); j++)
            {
                firstElement = tabel[0, j];
                while (i < tabel.GetLength(1))
                {
                    if (firstElement != tabel[i, j])
                        break;
                    if (i == tabel.GetLength(1) - 1)
                        check = true;
                    i++;
                }
                i = 0;
                if (check == true)
                    break;
            }
            return check;
        }
        static bool Check_win_diag(int[,] tabel)
        {
            bool diag_principal = Check_win_diag_principal(tabel);
            bool diag_secundar = Check_win_diag_secundar(tabel);
            if (diag_principal || diag_secundar)
                return true;
            return false;
        }
        static bool Check_win_diag_principal(int[,] tabel)
        {
            int firstElement = tabel[0, 0];
            int j = 0;

            while (j < tabel.GetLength(1))
            {
                if (tabel[j, j] != firstElement)
                    break;
                else
                    j++;
            }
            if (j == tabel.GetLength(1))
                return true;
            else
                return false;
        }
        static bool Check_win_diag_secundar(int[,] tabel)
        {
            int diag2elem = tabel[0, tabel.GetLength(1) - 1];
            int j;
            int i;
            bool check = false;

            for (i = 0; i < tabel.GetLength(0); i++)
            {
                for (j = tabel.GetLength(1) - 1; j >= 0; j--)
                {
                    if ((i + j) == tabel.GetLength(0) - 1)
                    {
                        if (tabel[i, j] != diag2elem)
                            return false;
                        else
                            check = true;
                    }
                }
            }
            return check;

        }


        static bool EmptyCell(int[,] tabel, int Square)
        {
            int square = Square - 1;
            bool isEmpty = false;
            int i = 0; int j = 0;

            for (i = 0; i < tabel.GetLength(0); i++)
            {

                if ((square / tabel.GetLength(0) == i))
                {
                    if (square % tabel.GetLength(0) == 0)
                    {
                        if (tabel[i, j] != 100 && tabel[i, j] != 200)   // check if cell is available.
                        {
                            isEmpty = true;
                            break;
                        }
                    }
                    for (j = 0; j < tabel.GetLength(1); j++)
                    {
                        if (square % tabel.GetLength(0) == j)
                        {
                            if (tabel[i, j] != 100 && tabel[i, j] != 200) // check if cell is available.
                            {
                                isEmpty = true;
                                break;
                            }
                        }
                    }
                }

            }
            return isEmpty;
        }

        static void Init_Table(int[,] tabel)
        {
            int temp = 1;

            for (int i = 0; i < tabel.GetLength(0); i++)
            {
                for (int j = 0; j < tabel.GetLength(1); j++)
                {
                    tabel[i, j] = temp;
                    temp++;
                }
            }
            PrintTable(tabel);
        }

        static void Test(int[,] tabel)
        {
            //Test method while building the program. 
            //Method to be decommisioned once finised.

            Test_Init_Table(tabel);

            bool test = false;
            Console.WriteLine(tabel.GetLength(1));

            test = Check_win_diag_principal(tabel);
            Console.WriteLine("diag princ " + test);

            test = Check_win_diag_secundar(tabel);
            Console.WriteLine("diag secund " + test);

            test = Check_win_diag(tabel);
            Console.WriteLine("diag " + test);

            test = Check_win_row(tabel);
            Console.WriteLine("row " + test);

            test = Check_win_coll(tabel);
            Console.WriteLine("col " + test);
        }
        static void Test_Init_Table(int[,] tabel)
        {

            for (int i = 0; i < tabel.GetLength(0); i++)
            {
                for (int j = 0; j < tabel.GetLength(1); j++)
                {
                    //if (i == 0 && j== (tabel.GetLength(1)-1))
                    //    tabel[i, j] = 0;
                    //else
                    //    tabel[i, j] = 1;
                    tabel[i, j] = RandomGenerator(0, 6);

                }
            }
            PrintTable(tabel);
        }

        public static int UserInputTableSize()
        {
            int lines;

            string userInput;
            bool success;
            Console.Clear();
            while (true)
            {
                Console.Write("Between 3 and 9 rows and columns how big would you like the table to be?: ");
                userInput = Console.ReadLine();
                success = int.TryParse(userInput, out lines);
                if (userInput.Length != 0 && success == true && lines > 2 && lines < 10)
                    break;
                Console.WriteLine("That's not a number between 3-9");
            }
            success = int.TryParse(userInput, out lines);
            return lines;
        }
        public static int UserInputTableCell(int[,] tabel)
        {
            string userInput;
            bool success; int square;

            while (true)
            {
                Console.Write("What square:");
                userInput = Console.ReadLine();
                success = int.TryParse(userInput, out square);
                if (userInput.Length != 0 && success == true && square != 0 && square <= (tabel.GetLength(0) * tabel.GetLength(1)) && EmptyCell(tabel, square))
                    break;
                Console.WriteLine("Thats not an available number in the table. Try again.");
            }
            success = int.TryParse(userInput, out square);
            return square;
        }

        public static int AiCheckLosingMove(int[,] tabel)
        {
            int square = -1;
            if (AiCheckLosingMoveRow(tabel) != -1)
                square = AiCheckLosingMoveRow(tabel);
            else if (AiCheckLosingMoveColumn(tabel) != -1)
                square = AiCheckLosingMoveColumn(tabel);
            else if (AiCheckLosingMoveDiagPrincipal(tabel) != -1)
                square = AiCheckLosingMoveDiagPrincipal(tabel);
            else if (AiCheckLosingMoveDiagSecundar(tabel) != -1)
                square = AiCheckLosingMoveDiagSecundar(tabel);
            return square;
        }
        // TBC
        public static int AiCheckLosingMoveRow(int[,] tabel)
        {
            int square = -1;
            int i = 0; int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;


            for (i = 0; i < tabel.GetLength(0); i++)
            {
                lastEmptySlot = -1;
                count = 0;

                for (j = 0; j < tabel.GetLength(1); j++)
                {
                    if (tabel[i, j] != 100 && tabel[i, j] != 200)
                        lastEmptySlot = tabel[i, j];
                    if (tabel[i, j] == 100)
                        count++;
                }
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1)
                {
                    square = lastEmptySlot;
                    check = true;
                    break;
                }
            }
            if (check)
                return square;
            else
                return -1;
        }
        public static int AiCheckLosingMoveColumn(int[,] tabel)
        {
            int square = -1;
            int i = 0; int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;

            for (j = 0; j < tabel.GetLength(0); j++)
            {
                lastEmptySlot = -1;
                count = 0;
                for (i = 0; i < tabel.GetLength(1); i++)
                {
                    if (tabel[i, j] != 100 && tabel[i, j] != 200)
                        lastEmptySlot = tabel[i, j];
                    if (tabel[i, j] == 100)
                        count++;
                }
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1)
                {
                    square = lastEmptySlot;
                    check = true;
                    break;
                }
            }
            if (check)
                return square;
            else
                return -1;
        }
        public static int AiCheckLosingMoveDiagPrincipal(int[,] tabel)
        {
            int square = -1;
            int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;

            while (j < tabel.GetLength(1))
            {
                if (tabel[j, j] != 100 && tabel[j, j] != 200)
                    lastEmptySlot = tabel[j, j];
                if (tabel[j, j] == 100)
                    count++;
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1)
                {
                    square = lastEmptySlot;
                    check = true;
                    break;
                }
                else
                    j++;
            }
            if (check)
                return square;
            else
                return -1;
        }
        public static int AiCheckLosingMoveDiagSecundar(int[,] tabel)
        {
            int diag2elem = tabel[0, tabel.GetLength(1) - 1];
            int j;
            int i;
            bool check = false;
            int lastEmptySlot = -1;
            int count = 0;
            int square = -1;

            for (i = 0; i < tabel.GetLength(0); i++)
            {
                for (j = tabel.GetLength(1) - 1; j >= 0; j--)
                {
                    if ((i + j) == tabel.GetLength(0) - 1)
                    {
                        if (tabel[i, j] != 100 && tabel[i, j] != 200)
                            lastEmptySlot = tabel[i, j];
                        if (tabel[i, j] == 100)
                            count++;
                        if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1)
                        {
                            square = lastEmptySlot;
                            check = true;
                            break;
                        }
                    }
                }
            }
            if (check)
                return square;
            else
                return -1;
        }

        public static int AiCheckWinningMove(int[,] tabel)
        {
            int square = -1;
            if (AiCheckWinningMoveRow(tabel) != -1)
                square = AiCheckWinningMoveRow(tabel);
            else if (AiCheckWinningMoveColumn(tabel) != -1)
                square = AiCheckWinningMoveColumn(tabel);
            else if (AiCheckWinningMoveDiagPrincipal(tabel) != -1)
                square = AiCheckWinningMoveDiagPrincipal(tabel);
            else if (AiCheckWinningMoveDiagSecundar(tabel) != -1)
                square = AiCheckWinningMoveDiagSecundar(tabel);
            return square;
        }

        public static int AiCheckWinningMoveRow(int[,] tabel)
        {
            int square = -1;
            int i = 0; int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;


            for (i = 0; i < tabel.GetLength(0); i++)
            {
                lastEmptySlot = -1;
                count = 0;

                for (j = 0; j < tabel.GetLength(1); j++)
                {
                    if (tabel[i, j] != 100 && tabel[i, j] != 200)
                        lastEmptySlot = tabel[i, j];
                    if (tabel[i, j] == 200)
                        count++;
                }
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1)
                {
                    square = lastEmptySlot;
                    check = true;
                    break;
                }
            }
            if (check)
                return square;
            else
                return -1;
        }
        public static int AiCheckWinningMoveColumn(int[,] tabel)
        {
            int square = -1;
            int i = 0; int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;

            for (j = 0; j < tabel.GetLength(0); j++)
            {
                lastEmptySlot = -1;
                count = 0;
                for (i = 0; i < tabel.GetLength(1); i++)
                {
                    if (tabel[i, j] != 100 && tabel[i, j] != 200)
                        lastEmptySlot = tabel[i, j];
                    if (tabel[i, j] == 200)
                        count++;
                }
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1)
                {
                    square = lastEmptySlot;
                    check = true;
                    break;
                }
            }
            if (check)
                return square;
            else
                return -1;
        }
        public static int AiCheckWinningMoveDiagPrincipal(int[,] tabel)
        {
            int square = -1;
            int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;

            while (j < tabel.GetLength(1))
            {
                if (tabel[j, j] != 100 && tabel[j, j] != 200)
                    lastEmptySlot = tabel[j, j];
                if (tabel[j, j] == 200)
                    count++;
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1)
                {
                    square = lastEmptySlot;
                    check = true;
                    break;
                }
                else
                    j++;
            }
            if (check)
                return square;
            else
                return -1;
        }
        public static int AiCheckWinningMoveDiagSecundar(int[,] tabel)
        {
            int diag2elem = tabel[0, tabel.GetLength(1) - 1];
            int j;
            int i;
            bool check = false;
            int lastEmptySlot = -1;
            int count = 0;
            int square = -1;

            for (i = 0; i < tabel.GetLength(0); i++)
            {
                for (j = tabel.GetLength(1) - 1; j >= 0; j--)
                {
                    if ((i + j) == tabel.GetLength(0) - 1)
                    {
                        if (tabel[i, j] != 100 && tabel[i, j] != 200)
                            lastEmptySlot = tabel[i, j];
                        if (tabel[i, j] == 200)
                            count++;
                        if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1)
                        {
                            square = lastEmptySlot;
                            check = true;
                            break;
                        }
                    }
                }
            }
            if (check)
                return square;
            else
                return -1;
        }

        public static int AiNextBestMove(int[,] tabel)   // to finish
        {
            int square = -1;
            int ai = 200;
            int iMid = tabel.GetLength(0) / 2;
            int jMid = tabel.GetLength(1) / 2;

            if (CheckIfCellIsEmpty(tabel, iMid, jMid))
                square = tabel[iMid, jMid];

            // check if diag does not contain enemy square
            // check rows that do not contain enemy square
            // check column that do not contain enemy square
            //

            if (tabel[tabel.GetLength(0) / 2, tabel.GetLength(1) / 2] == ai)
            {

            }
            return square;
        }

        public static bool CheckIfCellIsEmpty(int[,] tabel, int i, int j)
        {
            if (tabel[i, j] != 100 && tabel[i, j] != 200)
                return true;
            else
                return false;

        }

        public static int AiInputTableCell(int[,] tabel)
        {
            int square = 0;
            int i = 0; int j = 0;
            bool check = false;

            if (AiCheckWinningMove(tabel) != -1)
            {
                square = AiCheckWinningMove(tabel);
            }
            else if (AiCheckLosingMove(tabel) != -1)
            {
                square = AiCheckLosingMove(tabel);
            }
            else if (CheckIfCellIsEmpty(tabel, tabel.GetLength(0) / 2, tabel.GetLength(1) / 2))    // check middle of the table if available
            {                                                                               // to modify into next best              
                square = tabel[tabel.GetLength(0) / 2, tabel.GetLength(1) / 2];             //place instead of mid
            }
            else if (AiNextBestMove(tabel) != -1)
                square = AiNextBestMove(tabel);
            else
            {
                square = RandomAiInputTableCell(tabel);                                     //random available moves
                if (square == -1)
                {
                    //                                                                      // checks available moves
                    for (i = 0; i < tabel.GetLength(0); i++)
                    {
                        for (j = 0; j < tabel.GetLength(1); j++)
                        {
                            if (CheckIfCellIsEmpty(tabel, i, j))
                            {
                                square = tabel[i, j];
                                check = true;
                                break;
                            }
                        }
                        if (check == true)
                            break;
                    }
                    //}                                                                    // checks available moves
                }


            }

            return square;


        }

        public static int RandomAiInputTableCell(int[,] tabel)
        {
            int i, j;
            int square = 1;
            bool check = false;
            int counter = 0;
            int randomMaxTries = tabel.GetLength(0) * tabel.GetLength(1) + (tabel.GetLength(0)/2 * tabel.GetLength(1) /2 ) / 2;

            i = RandomGenerator(0, tabel.GetLength(0));
            j = RandomGenerator(0, tabel.GetLength(1));

            while (check == false)
            {
                if (counter == randomMaxTries)
                {
                    break;
                }
                if (CheckIfCellIsEmpty(tabel, i, j))
                {
                    square = tabel[i, j];
                    check = true;
                }
                i = RandomGenerator(0, tabel.GetLength(0));
                j = RandomGenerator(0, tabel.GetLength(1));
                counter++;

            }

            if (counter == randomMaxTries)
            {
                return -1;
            }
            else
                return square;

        }                       // returns a random open square or -1 if there is a long wait time.

        static void UpdateTableCell(int[,] tabel, int Square, int Player)
        {
            //Method checks each line to find the correct line. 
            //Once found it checks for the correct square to place the player or AI sign
            //once sign is placed into the table, it breaks out of the loop.

            //Method takes tabel to be able to modify the values of the table
            //Method takes Square, the place Player selected in the table
            //Method takes player. Might be temporary until AI function is defined.

            int FriendOrFoe = Player;
            int square = Square - 1;

            for (int i = 0; i < tabel.GetLength(0); i++)
            {

                if ((square / tabel.GetLength(0) == i))
                {
                    if (square % tabel.GetLength(0) == 0)
                    {
                        tabel[i, 0] = FriendOrFoe;
                        break;
                    }
                    for (int j = 0; j < tabel.GetLength(1); j++)
                    {

                        if (square % tabel.GetLength(0) == j)
                        {
                            tabel[i, j] = FriendOrFoe;
                            break;
                        }
                    }
                }

            }

        }
        ///
        static bool PlayAgain(string Message)
        {
            string userInput;
            while (true)
            {
                Console.WriteLine(Message);
                userInput = Console.ReadLine();
                userInput = userInput.ToUpper();
                if (userInput.Length != 0 && (userInput == "Y" || userInput == "N"))
                    break;
                Console.WriteLine("thats not Y/N");
            }
            if (userInput == "Y")
                return true;
            else
                return false;
        }
        static void ShowWinner(int[,] tabel)
        {

        }
        static bool CheckDraw(int[,] tabel)
        {
            int i, j;
            int availableSquares = tabel.GetLength(0) * tabel.GetLength(1);

            for (i = 0; i < tabel.GetLength(0); i++)
            {
                for (j = 0; j < tabel.GetLength(1); j++)
                {
                    if (tabel[i, j] == 100 || tabel[i, j] == 200)
                    {
                        availableSquares--;
                    }

                }
            }

            if (availableSquares == 0)
                return true;
            else
                return false;

        }
        static void Main(string[] args)
        {
            //var p='X'; 
            //var a = 'O'; 

            //TODO: User input for table cells (done)
            //TODO: AI - finish AI nextbestmove 
            //TODO: Draw condition (done)
            //TODO: ShowWinner 


            DefaultConsoleColor();
            int lines;
            int columns;
            int winnerRow;
            int winnerCollumn;
            int winnerDiag;

            bool isPlayerFirst = true;
            bool isDraw = false;
            int player;
            int AI;
            int winner = 0;

            string playAgain = "Would you like to play again? (Y/N)";
            string firstOrSecond = "Would you like to go first? (Y/N)";

            do // loops and at the end checks if you want to play again. 
            {
                //lines = 3; // testing purposes
                lines = UserInputTableSize();

                
                int[,] tabel = new int[lines, lines];
                {
                    winner = 0;
                    player = 100;
                    AI = 200;
                    isDraw = false;

                    // Init_table or Test to be run exclusively 
                    Init_Table(tabel);

                    //Test(tabel);

                    DefaultConsoleColor();

                    isPlayerFirst = PlayAgain(firstOrSecond); // player choses if he goes 1st  or 2nd 
                    //isPlayerFirst = false;
                    PrintTable(tabel);
                }

                {
                    do //gameplay loop - plays until winner is found
                    {

                        if (!isPlayerFirst)
                        {
                            isPlayerFirst = true;
                        }
                        else
                        {
                            UpdateTableCell(tabel, UserInputTableCell(tabel), player);
                            PrintTable(tabel);
                        }

                        if (CheckDraw(tabel))
                        {
                            isDraw = true;
                            break;
                        }
                        if (Check_win_all_casses(tabel))
                        {
                            winner = player;
                            break;
                        }

                        UpdateTableCell(tabel, AiInputTableCell(tabel), AI);
                        PrintTable(tabel);

                        if (CheckDraw(tabel))
                        {
                            isDraw = true;
                            break;
                        }

                    } while (!Check_win_all_casses(tabel));

                } // Player always first. 

                if (isDraw && winner == 0)
                {
                    Console.WriteLine("Now no one gets to be a winner. We are all losers!");
                }
                if (winner == player)
                {
                    Console.WriteLine("Congratz! You win!");
                }
                if (winner != player && isDraw == false)
                {
                    Console.WriteLine("Sucks to be you!");
                }
            } while (PlayAgain(playAgain));


























        }
    }
}
