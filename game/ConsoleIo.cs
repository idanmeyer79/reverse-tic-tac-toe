using System;
using System.Text;

namespace game
{
    internal class ConsoleIo
    {
        public static string DisplayBoard(Board i_Board)
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
            return sb.ToString();
        }

        public static int GetBoardSizeFromPlayer()
        {
            // Prompt the user to choose the board size
            bool isValidSize = false;
            int size;
            do
            {
                Console.Write("Enter the size of the board (3-9): ");
                if(!int.TryParse(Console.ReadLine(), out size) || size < 3 || size > 9)
                {
                    Console.Write("invalid input! try again\n");
                    continue;
                }

                isValidSize = true;
            } while (isValidSize == false);

            return size;
        }

        public static int GetMoveFromPlayer(int i_BoardSize)
        {
            bool isValidInput = false;
            int x;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out x) || x < 0 || x > i_BoardSize)
                {
                    Console.Write("invalid input! try again\n");
                    continue;
                }
                isValidInput = true;
            } while (isValidInput == false);

            return x;
        }

        public static void DisplaySummery(Player i_Player1, Player i_Player2)
        {

        }
    }
}
