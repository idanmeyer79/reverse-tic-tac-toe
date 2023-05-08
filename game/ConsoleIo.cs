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

            return new Player(name, i_Symbol);
        }

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
            bool playAgain = true;
            while (playAgain)
            {
                Console.WriteLine("Press 'N' to play again or 'Q' to quit the game");
                ConsoleKeyInfo key = Console.ReadKey();
                char answer = char.ToUpper(key.KeyChar);
                if (answer == 'N')
                {
                    break;
                }
                else if (answer == 'Q')
                {
                    playAgain = false;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please press 'N' to play again or 'Q' to quit the game");
                }
            }
            return playAgain;
        }

        public static int GetNumOfPlayers()
        {
            do
            {
                Console.Write("Enter number of players (1 or 2): ");
                string input = Console.ReadLine()?.Trim();
                if (int.TryParse(input, out int numPlayers) && (numPlayers == 1 || numPlayers == 2))
                {
                    return numPlayers;
                }
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
            } while (true);
        }

    }
}
