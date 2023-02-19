using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;



namespace TicTacToe {

    class Cell {
        public int Value;
        public bool IsTaken;
        public string IsTakenBy;

        public Cell(int Value, bool IsTaken, string IsTakenBy) {
            this.Value = Value;
            this.IsTaken = IsTaken;
            this.IsTakenBy = IsTakenBy;
        }

    }

    class Program {
        public static void CreateObjectTable(Cell[,] ObjectTable) {
            int temp = 1;
            for (int i = 0; i < ObjectTable.GetLength(0); i++) {
                for (int j = 0; j < ObjectTable.GetLength(1); j++) {
                    ObjectTable[i, j] = new Cell(temp, false, "none");
                    temp++;
                }
            }
            //PrintObjectTable(ObjectTable);
        }

        public static void PrintObjectTableNoBorder(Cell[,] ObjectTable) {
            for (int i = 0; i < ObjectTable.GetLength(0); i++) {
                for (int j = 0; j < ObjectTable.GetLength(1); j++) {
                    if (ObjectTable[i, j].IsTaken) {
                        Console.WriteLine(ObjectTable[i, j].IsTakenBy);
                    }
                    else {
                        Console.Write(ObjectTable[i, j].Value + " ");
                    }
                }
                Console.WriteLine("");
            }

        }

        public static bool CheckDraw(Cell[,] ObjectTable) {

            int availableSquares = ObjectTable.GetLength(0) * ObjectTable.GetLength(1);
            for (int i = 0; i < ObjectTable.GetLength(0); i++) {
                for (int j = 0; j < ObjectTable.GetLength(1); j++) {
                    if (ObjectTable[i, j].IsTaken)
                        return false;
                    else
                        availableSquares--;
                }
            }
            if (availableSquares == 0)
                return true;
            else
                return false;
        }
        public static int CheckWin(Cell[,] ObjectTable) /* returns -1 if no win or values of CheckWin functons below*/{

            int result = -1;
            result = CheckWinCol(ObjectTable);
            if (result != -1)
                return result;
            result = CheckWinRow(ObjectTable);
            if (result != -1)
                return result;
            result = CheckWinDiag(ObjectTable);
            if (result != -1)
                return result;
            return -1;
        }
        public static int CheckWinCol(Cell[,] ObjectTable) /* returns 20 + Col(J+1) if all elements of the col are identical or -1 if not win*/{
            int i = 0;
            int j = 0;
            bool check = false;

            //Col
            for (j = 0; j < ObjectTable.GetLength(1); j++) {
                string firstElem = ObjectTable[0, j].IsTakenBy;
                for (i = 0; i < ObjectTable.GetLength(0); i++) {
                    if (firstElem != ObjectTable[i, j].IsTakenBy) {
                        break;
                    }
                    if (j == ObjectTable.GetLength(1) - 1)
                        check = true;
                }
                if (check)
                    return 20 + j + 1;

            }
            return -1;
        }
        public static int CheckWinRow(Cell[,] ObjectTable) /* returns 10 + row(i+1) if all elements of the row are identical or -1 if not win*/{
            int i = 0;
            int j = 0;
            bool check = false;

            //row
            for (i = 0; i < ObjectTable.GetLength(0); i++) {
                string firstElem = ObjectTable[i, 0].IsTakenBy;
                for (j = 0; j < ObjectTable.GetLength(1); j++) {
                    if (firstElem != ObjectTable[i, j].IsTakenBy) {
                        break;
                    }
                    if (j == ObjectTable.GetLength(1) - 1)
                        check = true;
                }
                if (check)
                    return 10 + i + 1;

            }
            return -1;

        }
        public static int CheckWinDiag(Cell[,] ObjectTable) /* returns 31 for diag princ, 32 for diag sec or -1 if not win*/{

            if (CheckWinDiagPrinc(ObjectTable))
                return 31;
            else if (CheckWinDiagSec(ObjectTable))
                return 32;
            else
                return -1;
        }
        public static bool CheckWinDiagPrinc(Cell[,] ObjectTable) /*Check if winner is on diag princ */{
            string firstElement = ObjectTable[0, 0].IsTakenBy;
            int j = 0;

            while (j < ObjectTable.GetLength(1)) {
                if (ObjectTable[j, j].IsTakenBy != firstElement)
                    break;
                else
                    j++;
            }
            if (j == ObjectTable.GetLength(1))
                return true;
            else
                return false;
        }
        public static bool CheckWinDiagSec(Cell[,] ObjectTable) /*Check if winner is on diag sec */{


            string diag2elem = ObjectTable[0, ObjectTable.GetLength(1) - 1].IsTakenBy;
            int j;
            int i;
            bool check = false;

            for (i = 0; i < ObjectTable.GetLength(0); i++) {
                for (j = ObjectTable.GetLength(1) - 1; j >= 0; j--) {
                    if ((i + j) == ObjectTable.GetLength(0) - 1) {
                        if (ObjectTable[i, j].IsTakenBy != diag2elem)
                            return false;
                        else
                            check = true;
                    }
                }
            }
            return check;

        }
        public static int ShowWinner(int Value) /* returns winner block based on Value inputed.  */  {
            int value = Value;
            int temp = -1;

            if (value == -1)
                return -1;
            if (value > 30) {
                temp = value % 30;
                Console.WriteLine("on" + NumberToString(temp) + "diagonal");
            }
            else if (value > 20) {
                temp = value % 20;
                Console.WriteLine("on " + NumberToString(temp) + "row");
            }
            else if (value > 10) {
                temp = value % 10;
                Console.WriteLine("on " + NumberToString(temp) + "line");
            }
            return temp;
        }

        public static string NumberToString(int NumberToString) /* turns int 1,2,3,etc into 1st, 2nd, 3rd as string */ {
            if (NumberToString == 1)
                return "1st ";
            else if (NumberToString == 2)
                return "2nd ";
            else if (NumberToString == 3)
                return "3rd ";
            else
                return NumberToString + "th ";
        }

        static void PrintTableValue(Cell[,] ObjectTable, int i, int j) /* Called by PrintTable() -> Prints table values*/  {

            if (ObjectTable[i, j].IsTakenBy == "X") {
                ChangePlayerConsoleColor();
                Console.Write("X");
                DefaultConsoleColor();
            }
            else if (ObjectTable[i, j].IsTakenBy == "0") {
                ChangeAiConsoleColor();
                Console.Write("0");
                DefaultConsoleColor();
            }
            else {
                Console.Write(ObjectTable[i, j].Value);
            }
        }
        static void PrintTableBorder(Cell[,] ObjectTable, int length, int maxval) /* Called by PrintTable() -> Prints table borders*/{
            int digits = length;
            int Max = maxval;
            int j;

            for (j = 0; j < ObjectTable.GetLength(1); j++) {
                Console.Write("+-");
                for (int k = 1; k < digits; k++) {
                    Console.Write("--");
                    if (Max < 100)
                        Console.Write("-");
                }
                Console.Write("+");
            }
            Console.WriteLine();
        }
        static void PrintObjectTable(Cell[,] ObjectTable) /* prints table*/{
            //Console.Clear();
            int MaxValue = ObjectTable.GetLength(0) * ObjectTable.GetLength(1);
            int digits = MaxValue.ToString().Length;

            int i; int j;

            for (i = 0; i < ObjectTable.GetLength(0); i++) {
                PrintTableBorder(ObjectTable, digits, MaxValue); /// top line

                for (j = 0; j < ObjectTable.GetLength(1); j++) // mid line
                {
                    Console.Write("|");
                    for (int k = 1; k < digits; k++) {
                        Console.Write(" ");
                        if (ObjectTable[i, j].Value < 10 || ObjectTable[i, j].IsTakenBy == "X" || ObjectTable[i, j].IsTakenBy == "0") // add spaces for values
                            Console.Write(" ");
                    }

                    PrintTableValue(ObjectTable, i, j);         // print table value

                    for (int k = 1; k < digits; k++)
                        Console.Write(" ");
                    Console.Write("|");
                }
                Console.WriteLine();

                PrintTableBorder(ObjectTable, digits, MaxValue); //bot line
            }
        }

        static void GetCoordsOfSquare(Cell[,] ObjectTable, int Square)/* might be usefull TBD */ {

        }

        static void UpdateTableCell(Cell[,] ObjectTable, int Square, string Player) {
            //Method checks each line to find the correct line. 
            //Once found it checks for the correct square to place the player or AI sign
            //once sign is placed into the table, it breaks out of the loop.

            //Method takes tabel to be able to modify the values of the table
            //Method takes Square, the place Player selected in the table
            //Method takes player. Might be temporary until AI function is defined.

            int square = Square - 1;

            for (int i = 0; i < ObjectTable.GetLength(0); i++) {

                if ((square / ObjectTable.GetLength(0) == i)) {
                    if (square % ObjectTable.GetLength(0) == 0) {
                        AssignCell(ObjectTable, i,  0, Player);
                        break;
                    }
                    for (int j = 0; j < ObjectTable.GetLength(1); j++) {

                        if (square % ObjectTable.GetLength(0) == j) {
                            AssignCell(ObjectTable, i, j, Player);
                                
                            break;
                        }
                    }
                }

            }

        }
        public static void AssignCell(Cell[,] ObjectTable, int i, int j, string player) {
            ObjectTable[i, j].IsTaken = true;
            ObjectTable[i, j].IsTakenBy = player;
        }
        public static void UnAssignCell(Cell[,] ObjectTable, int i, int j) {
            ObjectTable[i, j].IsTaken = false;
            ObjectTable[i, j].IsTakenBy = "none";
        }

        static bool IsTaken(Cell[,] ObjectTable, int Square) {
            //Method checks each line to find the correct line. 
            //Once found it checks for the correct square to place the player or AI sign
            //once sign is placed into the table, it breaks out of the loop.

            //Method takes tabel to be able to modify the values of the table
            //Method takes Square, the place Player selected in the table
            //Method takes player. Might be temporary until AI function is defined.

            int square = Square - 1;

            for (int i = 0; i < ObjectTable.GetLength(0); i++) {

                if ((square / ObjectTable.GetLength(0) == i)) {
                    if (square % ObjectTable.GetLength(0) == 0) {
                        if (ObjectTable[i, 0].IsTaken)
                            return true;
                    }
                    for (int j = 0; j < ObjectTable.GetLength(1); j++) {

                        if (square % ObjectTable.GetLength(0) == j) {
                            if (ObjectTable[i, j].IsTaken)
                                return true;
                        }
                    }
                }
            }
            return false;

        }

        public static int AIInputTableCell(Cell[,] ObjectTable, string Player) {
            int square = 0;
            int i = 0; int j = 0;
            bool check = false;

            MiniMax(ObjectTable, Player, true);


            //if (AiCheckWinningMove(ObjectTable) != -1) {
            //    square = AiCheckWinningMove(ObjectTable);
            //}
            //else if (AiCheckLosingMove(ObjectTable) != -1) {
            //    square = AiCheckLosingMove(ObjectTable);
            //}
            //else if (CheckIfCellIsEmpty(ObjectTable, ObjectTable.GetLength(0) / 2, ObjectTable.GetLength(1) / 2))    // check middle of the table if available
            //{                                                                               // to modify into next best              
            //    square = ObjectTable[ObjectTable.GetLength(0) / 2, ObjectTable.GetLength(1) / 2];             //place instead of mid
            //}
            //else if (AiNextBestMove(ObjectTable) != -1)
            //    square = AiNextBestMove(ObjectTable);
            //else {
            //    square = RandomAiInputTableCell(ObjectTable);                                     //random available moves
            //    if (square == -1) {
            //        //                                                                      // checks available moves
            //        for (i = 0; i < ObjectTable.GetLength(0); i++) {
            //            for (j = 0; j < ObjectTable.GetLength(1); j++) {
            //                if (CheckIfCellIsEmpty(ObjectTable, i, j)) {
            //                    square = ObjectTable[i, j];
            //                    check = true;
            //                    break;
            //                }
            //            }
            //            if (check == true)
            //                break;
            //        }
            //        //}                                                                    // checks available moves
            //    }


            //}

            return square;
            
        }

        public static int UserInputTableCell(Cell[,] ObjectTable) /* potential optimization possible. TBI*/{
            string userInput;
            bool success; int square;

            while (true) {
                Console.Write("What square:");
                userInput = Console.ReadLine();
                success = int.TryParse(userInput, out square);
                if (userInput.Length != 0 && success == true && square != 0 && square <= (ObjectTable.GetLength(0) * ObjectTable.GetLength(1)) && !IsTaken(ObjectTable, square))
                    break;
                Console.WriteLine("Thats not an available number in the table. Try again.");
            }
            success = int.TryParse(userInput, out square);
            return square;
        }

        public static int MiniMax(Cell[,] ObjectTable, string Player, bool MaximizingPlayer) {
            // winner condition and return values
            if (CheckDraw(ObjectTable))
                return 0;
            else if (CheckWin(ObjectTable) != -1)
                return 1;
            else if (CheckWin(ObjectTable) == -1)
                return -1;


            int MaxEval = int.MinValue;
            int MinEval = int.MaxValue;
            string playerNow;

            if (Player == "X")
                playerNow = "X";
            else
                playerNow = "0";


            if (MaximizingPlayer) {
                if (playerNow == "0")          // this might need changing
                    playerNow = "X";
                else
                    playerNow = "0";
                for (int i = 0; i < ObjectTable.GetLength(0); i++) {            // Maximizing player - Checks for his best score.
                    for (int j = 0; j < ObjectTable.GetLength(1); j++) {
                        if (!ObjectTable[i, j].IsTaken) {

                            AssignCell(ObjectTable, i, j, playerNow);
                            int currentEval = MiniMax(ObjectTable, playerNow, false);
                            UnAssignCell(ObjectTable, i, j);

                            if (currentEval > MaxEval) {
                                MaxEval = currentEval;
                            }
                            if (currentEval < MinEval) {
                                MinEval = currentEval;
                            }

                        }

                    }
                }
            }
            else {
                if (playerNow == "X")              // this might need changing
                    playerNow = "0";
                else
                    playerNow = "X";

                for (int i = 0; i < ObjectTable.GetLength(0); i++) {            // Minimizing player - Checks for oponents.
                    for (int j = 0; j < ObjectTable.GetLength(1); j++) {
                        if (!ObjectTable[i, j].IsTaken) {

                            AssignCell(ObjectTable, i, j, playerNow);
                            int currentEval = MiniMax(ObjectTable, playerNow, true);
                            UnAssignCell(ObjectTable, i, j);

                            if (currentEval > MaxEval) {
                                MaxEval = currentEval;
                            }
                            if (currentEval < MinEval) {
                                MinEval = currentEval;
                            }

                        }

                    }
                }



            }

            if (MaximizingPlayer) { // this might need changing 
                return MaxEval;
            }
            else {
                return MinEval;
            }

        }

        static void DefaultConsoleColor() {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }
        static void ChangePlayerConsoleColor() {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        static void ChangeAiConsoleColor() {
            Console.ForegroundColor = ConsoleColor.Red;
        }


        static int RandomGenerator(int NumberMin, int NumberMax) {
            int val;
            int min = NumberMin;
            int max = NumberMax;
            // System.Threading.Thread.Sleep(1);
            Random random = new Random();
            val = random.Next(min, max);
            return val;
        }

        static void PrintTableValue(int[,] tabel, int i, int j) {
            int Player = 100;
            int AI = 200;
            if (tabel[i, j] == Player) {
                ChangePlayerConsoleColor();
                Console.Write("X");
                DefaultConsoleColor();
            }
            else if (tabel[i, j] == AI) {
                ChangeAiConsoleColor();
                Console.Write("0");
                DefaultConsoleColor();
            }
            else {
                Console.Write(tabel[i, j]);
            }
        }
        static void PrintTableBorder(int[,] tabel, int length, int maxval) {
            int digits = length;
            int Max = maxval;
            int j;

            for (j = 0; j < tabel.GetLength(1); j++) {
                Console.Write("+-");
                for (int k = 1; k < digits; k++) {
                    Console.Write("--");
                    if (Max < 100)
                        Console.Write("-");
                }
                Console.Write("+");
            }
            Console.WriteLine();
        }
        static void PrintTable(int[,] tabel) {
            Console.Clear();
            int MaxValue = tabel.GetLength(0) * tabel.GetLength(1);
            int digits = MaxValue.ToString().Length;

            int i; int j;

            for (i = 0; i < tabel.GetLength(0); i++) {
                PrintTableBorder(tabel, digits, MaxValue); /// top line

                for (j = 0; j < tabel.GetLength(1); j++) // mid line
                {
                    Console.Write("|");
                    for (int k = 1; k < digits; k++) {
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

        static bool Check_win_all_casses(int[,] tabel) {
            // function incorporates all other checks for a winner 
            // if any of the other functions return true, this function returns true

            if (Check_win_diag(tabel) || Check_win_row(tabel) || Check_win_coll(tabel))
                return true;
            else
                return false;
        }
        static bool Check_win_row(int[,] tabel) {
            int firstElement = tabel[0, 0];
            int j;
            int i;
            bool check = false;

            for (i = 0; i < tabel.GetLength(0); i++) {
                firstElement = tabel[i, 0];
                j = 0;
                while (j < tabel.GetLength(1)) {
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
        static bool Check_win_coll(int[,] tabel) {
            int firstElement = tabel[0, 0];
            int j = 0;
            int i = 0;
            bool check = false;

            for (j = 0; j < tabel.GetLength(0); j++) {
                firstElement = tabel[0, j];
                while (i < tabel.GetLength(1)) {
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
        static bool Check_win_diag(int[,] tabel) {
            bool diag_principal = Check_win_diag_principal(tabel);
            bool diag_secundar = Check_win_diag_secundar(tabel);
            if (diag_principal || diag_secundar)
                return true;
            return false;
        }
        static bool Check_win_diag_principal(int[,] tabel) {
            int firstElement = tabel[0, 0];
            int j = 0;

            while (j < tabel.GetLength(1)) {
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
        static bool Check_win_diag_secundar(int[,] tabel) {
            int diag2elem = tabel[0, tabel.GetLength(1) - 1];
            int j;
            int i;
            bool check = false;

            for (i = 0; i < tabel.GetLength(0); i++) {
                for (j = tabel.GetLength(1) - 1; j >= 0; j--) {
                    if ((i + j) == tabel.GetLength(0) - 1) {
                        if (tabel[i, j] != diag2elem)
                            return false;
                        else
                            check = true;
                    }
                }
            }
            return check;

        }


        static bool EmptyCell(int[,] tabel, int Square) {
            int square = Square - 1;
            bool isEmpty = false;
            int i = 0; int j = 0;

            for (i = 0; i < tabel.GetLength(0); i++) {

                if ((square / tabel.GetLength(0) == i)) {
                    if (square % tabel.GetLength(0) == 0) {
                        if (tabel[i, j] != 100 && tabel[i, j] != 200)   // check if cell is available.
                        {
                            isEmpty = true;
                            break;
                        }
                    }
                    for (j = 0; j < tabel.GetLength(1); j++) {
                        if (square % tabel.GetLength(0) == j) {
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

        static void Init_Table(int[,] tabel) {
            int temp = 1;

            for (int i = 0; i < tabel.GetLength(0); i++) {
                for (int j = 0; j < tabel.GetLength(1); j++) {
                    tabel[i, j] = temp;
                    temp++;
                }
            }
            PrintTable(tabel);
        }

        static void Test(int[,] tabel) {
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
        static void Test_Init_Table(int[,] tabel) {

            for (int i = 0; i < tabel.GetLength(0); i++) {
                for (int j = 0; j < tabel.GetLength(1); j++) {
                    //if (i == 0 && j== (tabel.GetLength(1)-1))
                    //    tabel[i, j] = 0;
                    //else
                    //    tabel[i, j] = 1;
                    tabel[i, j] = RandomGenerator(0, 6);

                }
            }
            PrintTable(tabel);
        }

        public static int UserInputTableSize() {
            int lines;

            string userInput;
            bool success;
            Console.Clear();
            while (true) {
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
        public static int UserInputTableCell(int[,] tabel) {
            string userInput;
            bool success; int square;

            while (true) {
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

        public static int AiCheckLosingMove(int[,] tabel) {
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
        public static int AiCheckLosingMoveRow(int[,] tabel) {
            int square = -1;
            int i = 0; int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;


            for (i = 0; i < tabel.GetLength(0); i++) {
                lastEmptySlot = -1;
                count = 0;

                for (j = 0; j < tabel.GetLength(1); j++) {
                    if (tabel[i, j] != 100 && tabel[i, j] != 200)
                        lastEmptySlot = tabel[i, j];
                    if (tabel[i, j] == 100)
                        count++;
                }
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1) {
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
        public static int AiCheckLosingMoveColumn(int[,] tabel) {
            int square = -1;
            int i = 0; int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;

            for (j = 0; j < tabel.GetLength(0); j++) {
                lastEmptySlot = -1;
                count = 0;
                for (i = 0; i < tabel.GetLength(1); i++) {
                    if (tabel[i, j] != 100 && tabel[i, j] != 200)
                        lastEmptySlot = tabel[i, j];
                    if (tabel[i, j] == 100)
                        count++;
                }
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1) {
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
        public static int AiCheckLosingMoveDiagPrincipal(int[,] tabel) {
            int square = -1;
            int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;

            while (j < tabel.GetLength(1)) {
                if (tabel[j, j] != 100 && tabel[j, j] != 200)
                    lastEmptySlot = tabel[j, j];
                if (tabel[j, j] == 100)
                    count++;
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1) {
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
        public static int AiCheckLosingMoveDiagSecundar(int[,] tabel) {
            int diag2elem = tabel[0, tabel.GetLength(1) - 1];
            int j;
            int i;
            bool check = false;
            int lastEmptySlot = -1;
            int count = 0;
            int square = -1;

            for (i = 0; i < tabel.GetLength(0); i++) {
                for (j = tabel.GetLength(1) - 1; j >= 0; j--) {
                    if ((i + j) == tabel.GetLength(0) - 1) {
                        if (tabel[i, j] != 100 && tabel[i, j] != 200)
                            lastEmptySlot = tabel[i, j];
                        if (tabel[i, j] == 100)
                            count++;
                        if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1) {
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

        public static int AiCheckWinningMove(int[,] tabel) {
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

        public static int AiCheckWinningMoveRow(int[,] tabel) {
            int square = -1;
            int i = 0; int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;


            for (i = 0; i < tabel.GetLength(0); i++) {
                lastEmptySlot = -1;
                count = 0;

                for (j = 0; j < tabel.GetLength(1); j++) {
                    if (tabel[i, j] != 100 && tabel[i, j] != 200)
                        lastEmptySlot = tabel[i, j];
                    if (tabel[i, j] == 200)
                        count++;
                }
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1) {
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
        public static int AiCheckWinningMoveColumn(int[,] tabel) {
            int square = -1;
            int i = 0; int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;

            for (j = 0; j < tabel.GetLength(0); j++) {
                lastEmptySlot = -1;
                count = 0;
                for (i = 0; i < tabel.GetLength(1); i++) {
                    if (tabel[i, j] != 100 && tabel[i, j] != 200)
                        lastEmptySlot = tabel[i, j];
                    if (tabel[i, j] == 200)
                        count++;
                }
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1) {
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
        public static int AiCheckWinningMoveDiagPrincipal(int[,] tabel) {
            int square = -1;
            int j = 0;
            int count = 0;
            int lastEmptySlot = -1;
            bool check = false;

            while (j < tabel.GetLength(1)) {
                if (tabel[j, j] != 100 && tabel[j, j] != 200)
                    lastEmptySlot = tabel[j, j];
                if (tabel[j, j] == 200)
                    count++;
                if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1) {
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
        public static int AiCheckWinningMoveDiagSecundar(int[,] tabel) {
            int diag2elem = tabel[0, tabel.GetLength(1) - 1];
            int j;
            int i;
            bool check = false;
            int lastEmptySlot = -1;
            int count = 0;
            int square = -1;

            for (i = 0; i < tabel.GetLength(0); i++) {
                for (j = tabel.GetLength(1) - 1; j >= 0; j--) {
                    if ((i + j) == tabel.GetLength(0) - 1) {
                        if (tabel[i, j] != 100 && tabel[i, j] != 200)
                            lastEmptySlot = tabel[i, j];
                        if (tabel[i, j] == 200)
                            count++;
                        if (count == tabel.GetLength(1) - 1 && lastEmptySlot != -1) {
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

            if (tabel[tabel.GetLength(0) / 2, tabel.GetLength(1) / 2] == ai) {

            }
            return square;
        }

        public static bool CheckIfCellIsEmpty(int[,] tabel, int i, int j) {
            if (tabel[i, j] != 100 && tabel[i, j] != 200)
                return true;
            else
                return false;

        }

        public static int AiInputTableCell(int[,] tabel) {
            int square = 0;
            int i = 0; int j = 0;
            bool check = false;

            if (AiCheckWinningMove(tabel) != -1) {
                square = AiCheckWinningMove(tabel);
            }
            else if (AiCheckLosingMove(tabel) != -1) {
                square = AiCheckLosingMove(tabel);
            }
            else if (CheckIfCellIsEmpty(tabel, tabel.GetLength(0) / 2, tabel.GetLength(1) / 2))    // check middle of the table if available
            {                                                                               // to modify into next best              
                square = tabel[tabel.GetLength(0) / 2, tabel.GetLength(1) / 2];             //place instead of mid
            }
            else if (AiNextBestMove(tabel) != -1)
                square = AiNextBestMove(tabel);
            else {
                square = RandomAiInputTableCell(tabel);                                     //random available moves
                if (square == -1) {
                    //                                                                      // checks available moves
                    for (i = 0; i < tabel.GetLength(0); i++) {
                        for (j = 0; j < tabel.GetLength(1); j++) {
                            if (CheckIfCellIsEmpty(tabel, i, j)) {
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

        public static int RandomAiInputTableCell(int[,] tabel) {
            int i, j;
            int square = 1;
            bool check = false;
            int counter = 0;
            int randomMaxTries = tabel.GetLength(0) * tabel.GetLength(1) + (tabel.GetLength(0) / 2 * tabel.GetLength(1) / 2) / 2;

            i = RandomGenerator(0, tabel.GetLength(0));
            j = RandomGenerator(0, tabel.GetLength(1));

            while (check == false) {
                if (counter == randomMaxTries) {
                    break;
                }
                if (CheckIfCellIsEmpty(tabel, i, j)) {
                    square = tabel[i, j];
                    check = true;
                }
                i = RandomGenerator(0, tabel.GetLength(0));
                j = RandomGenerator(0, tabel.GetLength(1));
                counter++;

            }

            if (counter == randomMaxTries) {
                return -1;
            }
            else
                return square;

        }                       // returns a random open square or -1 if there is a long wait time.

        static void UpdateTableCell(int[,] tabel, int Square, int Player) {
            //Method checks each line to find the correct line. 
            //Once found it checks for the correct square to place the player or AI sign
            //once sign is placed into the table, it breaks out of the loop.

            //Method takes tabel to be able to modify the values of the table
            //Method takes Square, the place Player selected in the table
            //Method takes player. Might be temporary until AI function is defined.

            int FriendOrFoe = Player;
            int square = Square - 1;

            for (int i = 0; i < tabel.GetLength(0); i++) {

                if ((square / tabel.GetLength(0) == i)) {
                    if (square % tabel.GetLength(0) == 0) {
                        tabel[i, 0] = FriendOrFoe;
                        break;
                    }
                    for (int j = 0; j < tabel.GetLength(1); j++) {

                        if (square % tabel.GetLength(0) == j) {
                            tabel[i, j] = FriendOrFoe;
                            break;
                        }
                    }
                }

            }

        }
        ///
        static bool UserInputString(string Message) {
            string userInput;
            while (true) {
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
        static void ShowWinner(int[,] tabel) {

        }
        static bool CheckDraw(int[,] tabel) {
            int i, j;
            int availableSquares = tabel.GetLength(0) * tabel.GetLength(1);

            for (i = 0; i < tabel.GetLength(0); i++) {
                for (j = 0; j < tabel.GetLength(1); j++) {
                    if (tabel[i, j] == 100 || tabel[i, j] == 200) {
                        availableSquares--;
                    }
                    else {
                        return false;
                    }


                }
            }

            if (availableSquares == 0)
                return true;
            else
                return false;

        }
        static void Main(string[] args) {
            //var p='X'; 
            //var a = 'O'; 

            //TODO: User input for table cells (done)
            //TODO: AI - finish AI nextbestmove 
            //TODO: Draw condition (done)
            //TODO: ShowWinner 



            DefaultConsoleColor();


            int lines;
            int columns;


            bool isPlayerFirst = true;
            bool isDraw = false;
            int player; string stringPlayer;
            int AI; string stringAI;
            int winner = 0;

            string playAgain = "Would you like to play again? (Y/N)";
            string firstOrSecond = "Would you like to go first? (Y/N)";

            do // loops and at the end checks if you want to play again. 
            {

                //lines = UserInputTableSize();
                lines = 3; // testing purposes

                int[,] tabel = new int[lines, lines];
                Cell[,] ObjectTable = new Cell[lines, lines];




                {
                    winner = 0;
                    player = 100;
                    stringPlayer = "X";
                    AI = 200;
                    stringAI = "0";
                    isDraw = false;

                    // Init_table or Test to be run exclusively 

                    Init_Table(tabel);
                    CreateObjectTable(ObjectTable);
                    
                    //Test(tabel);

                    DefaultConsoleColor();

                    isPlayerFirst = UserInputString(firstOrSecond);             // player choses if he goes 1st  or 2nd 
                                                                                //isPlayerFirst = false;
                                                                                //Console.WriteLine("Normal Table"); 
                    PrintTable(tabel);
                    //Console.WriteLine("Object Table"); 
                    //PrintObjectTable(ObjectTable);
                }

                var turncount = 0;
                if (isPlayerFirst) {
                    turncount = 1;
                }
                
                {
                    do {                                                     //gameplay loop - plays until winner is found
                        if (!isPlayerFirst && turncount == 0) {

                        }
                        else {
                            var userInput = UserInputTableCell(ObjectTable);
                            UpdateTableCell(tabel, userInput, player);
                            //UpdateTableCell(ObjectTable, userInput, stringPlayer);
                            //Console.WriteLine("Normal Table"); 
                            PrintTable(tabel);
                            //Console.WriteLine("Object Table");

                            //PrintObjectTable(ObjectTable);
                        }


                        if (CheckDraw(tabel)) {
                            isDraw = true;
                            break;
                        }
                        if (Check_win_all_casses(tabel)) {
                            winner = player;
                            break;
                        }

                        UpdateTableCell(tabel, AiInputTableCell(tabel), AI);
                        PrintTable(tabel);
                        //UpdateTableCell(ObjectTable, UserInputTableCell(ObjectTable), stringAI);
                        PrintTable(tabel);
                        //PrintObjectTable(ObjectTable);

                        if (CheckDraw(tabel)) {
                            isDraw = true;
                            break;
                        }
                        turncount++;
                    } while (!Check_win_all_casses(tabel));

                } // Player always first. 

                if (isDraw && winner == 0) {
                    Console.WriteLine("Now no one gets to be a winner. We are all losers!");
                }
                if (winner == player) {
                    Console.WriteLine("Congratz! You win!");
                }
                if (winner != player && isDraw == false) {
                    Console.WriteLine("Sucks to be you!");
                }
            } while (UserInputString(playAgain));



        }
    }
}
