using System;
using System.Text;


namespace game
{
    internal class ConsoleIO
    {
        public const int Quit = -1;

        public int WelcomeUserAndGetNumOfPlayers()
        {
            Console.WriteLine("Welcome to Reverse Tic Tac Toe!");

            return GetNumOfPlayers();
        }

        public Player GetPlayerDetailsFromUser(char i_Symbol)
        {
            Console.Write("Enter player name: ");
            string name = Console.ReadLine()?.Trim();

            return new Player(name, i_Symbol);
        }

        public void DisplayWhoseTurn(string i_NameOfCurrentPlayer, char i_Symbol)
        {
            Console.WriteLine($"It's {i_NameOfCurrentPlayer}'s turn ({i_Symbol})");
        }

        public void DisplayBoard(BoardGame i_Board)
        {
            StringBuilder sb = new StringBuilder();

            Ex02.ConsoleUtils.Screen.Clear();
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

        public void DisplayCellIsOccupiedMsg()
        {
            Console.Write("Cell is already occupied! Please choose an empty cell.\n");
        }

        public int GetBoardSizeFromPlayer()
        {
            // Prompt the user to choose the board size
            bool isValidSize = false;
            int size;
            do
            {
                Console.Write($"Enter the size of the board ({BoardGame.MinSizeOfBoard}-{BoardGame.MaxSizeOfBoard}): ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out size) || size < BoardGame.MinSizeOfBoard || size > BoardGame.MaxSizeOfBoard)
                {
                    Console.Write("invalid input! try again\n");
                    continue;
                }

                isValidSize = true;
            } while (isValidSize == false);

            return size;
        }

        public bool GetMoveFromPlayer(int i_BoardSize, out int o_row, out int o_col)
        {
            bool doesPlayerWantToQuit = true;

            o_row = GetFromUserBoardValueOfDimension(i_BoardSize, "row");
            if (o_row != Quit)
            {
                o_col = GetFromUserBoardValueOfDimension(i_BoardSize, "column");
                if(o_col != Quit)
                {
                    doesPlayerWantToQuit = false;
                }
            }
            else
            {
                o_col = Quit;
            }

            return doesPlayerWantToQuit;
        }

        public int GetFromUserBoardValueOfDimension(int i_BoardSize, string dimension)
        {
            int valueOfDimension;
            bool isValidInput = false;
            do
            {
                Console.Write($"Enter the {dimension} number of your move (1-{i_BoardSize}): ");
                string input = Console.ReadLine().Trim();
                if (CheckInputForQuittingTheGame(input))
                {
                    valueOfDimension = Quit;
                    isValidInput = true;
                }
                else if (!int.TryParse(input, out valueOfDimension) || valueOfDimension < BoardGame.MinValOfDimension || valueOfDimension > i_BoardSize)
                {
                    Console.Write($"Invalid input! Please enter a valid {dimension} number.\n");
                }
                else
                {
                    isValidInput = true;
                }
            } while (!isValidInput);

            return valueOfDimension;
        }

        public void DisplaySummary(GameLogic i_Game)
        {
            if(i_Game.Player1.Forfeited)
            {
                Console.WriteLine($"{i_Game.Player1.Name} forfeits!");
            }
            else if(i_Game.Player2.Forfeited)
            {
                Console.WriteLine($"{i_Game.Player2.Name} forfeits!");
            }
            else if (i_Game.Winner == null)
            {
                Console.WriteLine("It's a tie!");
            }
            else
            {
                Console.WriteLine($"{i_Game.Winner.Name} ({i_Game.Winner.Symbol}) won!");
            }

            Console.WriteLine($@"Score summary:
{i_Game.Player1.Name} has {i_Game.Player1.Score} points.
{i_Game.Player2.Name} has {i_Game.Player2.Score} points.");
        }

        public bool DoesPlayerWantToPlayAnotherRound()
        {
            bool result = true;

            Console.WriteLine("Press any key to play again or 'Q' to quit the game");
            string input = Console.ReadLine();
            if(CheckInputForQuittingTheGame(input))
            {
                result = false;
            }

            return result;
        }

        public int GetNumOfPlayers()
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

        public bool CheckInputForQuittingTheGame(string i_Input)
        {
            return i_Input == "Q";
        }
        public void DisplayTheFinalBoardAndSummary(GameLogic i_Game)
        {
            DisplayBoard(i_Game.Board);
            DisplaySummary(i_Game);
        }
    }
}
