using System;
using System.Text;

namespace game
{
    internal class ConsoleIo
    {
        public static Player GetPlayerDetailsFromUser(char i_Symbol)
        {
            Console.Write("Enter player name: ");
            string name = Console.ReadLine()?.Trim();
            CheckInputForQ(name);

            return new Player(name, i_Symbol);
        }

        public static void DisplayBoard(Board i_Board)
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < i_Board.BoardSize; j++)
            {
                sb.Append($"  {j + 1} ");
            }
            sb.AppendLine();
            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                sb.Append($"{i + 1}|");
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    sb.Append($" {i_Board.Cells[i, j].Symbol} ");
                    if (j < i_Board.BoardSize - 1)
                    {
                        sb.Append("|");
                    }
                }
                sb.Append("|");
                sb.AppendLine();
                if (i <= i_Board.BoardSize)
                {
                    sb.Append(" ");
                    sb.Append(new string('=', i_Board.BoardSize * 4 + 1));
                    sb.AppendLine();
                }
            }
            Console.WriteLine(sb.ToString());
        }

        public static int GetBoardSizeFromPlayer()
        {
            // Prompt the user to choose the board size
            bool isValidSize = false;
            int size;
            do
            {
                Console.Write("Enter the size of the board (3-9): ");
                string input = Console.ReadLine();
                CheckInputForQ(input);
                if (!int.TryParse(input, out size) || size < 3 || size > 9)
                {
                    Console.Write("invalid input! try again\n");
                    continue;
                }

                isValidSize = true;
            } while (isValidSize == false);

            return size;
        }

        public static void GetMoveFromPlayer(Board i_Board, out int o_row, out int o_col)
        {
            o_row = -1;
            o_col = -1;
            bool isValidInput = false;
            do
            {
                Console.Write($"Enter the row number of your move (1-{i_Board.BoardSize}): ");
                string input = Console.ReadLine().Trim();
                CheckInputForQ(input);
                if (!int.TryParse(input, out o_row) || o_row < 1 || o_row > i_Board.BoardSize)
                {
                    Console.Write("Invalid input! Please enter a valid row number.\n");
                    continue;
                }

                Console.Write($"Enter the column number of your move (1-{i_Board.BoardSize}): ");
                input = Console.ReadLine().Trim();
                CheckInputForQ(input);
                if (!int.TryParse(input, out o_col) || o_col < 1 || o_col > i_Board.BoardSize)
                {
                    Console.Write("Invalid input! Please enter a valid column number.\n");
                    continue;
                }

                if (i_Board.GetCellSymbol(o_row - 1, o_col - 1) != ' ')
                {
                    Console.Write("Cell is already occupied! Please choose an empty cell.\n");
                    continue;
                }

                isValidInput = true;
            } while (!isValidInput);
        }



        public static void DisplaySummery(Game i_Game)
        {
            if (i_Game.Winner == null)
            {
                Console.WriteLine("It's a tie!");
            }
            else
            {
                Console.WriteLine($"Congratulations, {i_Game.Winner.Name} ({i_Game.Winner.Symbol})! You won!");
            }

            Console.WriteLine($@"Score summery:
{i_Game.Player1.Name} has {i_Game.Player1.Score} points.
{i_Game.Player2.Name} has {i_Game.Player2.Score} points");
        }

        public static bool AskForUserToPlayNextRound()
        {
            Console.WriteLine("Press any key to play again or 'Q' to quit the game");
            string input = Console.ReadLine();
            CheckInputForQ(input);
            return true;
        }

        public static int GetNumOfPlayers()
        {
            do
            {
                Console.Write("Enter number of players (1 or 2): ");
                string input = Console.ReadLine()?.Trim();
                CheckInputForQ(input);
                if (int.TryParse(input, out int numPlayers) && (numPlayers == 1 || numPlayers == 2))
                {
                    return numPlayers;
                }
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
            } while (true);
        }

        public static void CheckInputForQ(string i_Input)
        {
            if (i_Input == "Q")
            {
                Environment.Exit(0);
            }
        }

    }
}
