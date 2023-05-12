using System;

namespace game
{
    internal class Game
    {
        public const int Quit = -1;
        public const int FirstRound = 0;
        public const char Empty = ' ';
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer { get; set;}
        public Player Winner { get; set; }
        public Board Board { get; set; }
        public int NumOfRounds { get; set; }
        public bool IsRoundOver { get; set; }

        // Ctor
        public Game()
        {
            int numOfPlayers = ConsoleIO.WelcomeUserAndGetNumOfPlayers(this);
            // Create the players
            Player1 = ConsoleIO.GetPlayerDetailsFromUser('X');

            if (numOfPlayers == 1)
            {
                Player2 = new Player("Computer", 'O')
                {
                    IsComputer = true
                };
            }
            else
            {
                Player2 = ConsoleIO.GetPlayerDetailsFromUser('O');
            }

            CurrentPlayer = (NumOfRounds) % 2 == 0 ? Player1 : Player2;

            int boardSize = ConsoleIO.GetBoardSizeFromPlayer();

            Board = new Board(boardSize);

        }

        public void RunGame()
        {
            do
            {
                if(NumOfRounds++ != FirstRound)
                {
                    SetGameForNewRound();
                }
                while(!IsRoundOver)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    ConsoleIO.DisplayBoard(Board);
                    PlayTurn();
                }
                DisplayTheFinalBoardAndSummary();
            }
            while (ConsoleIO.DoesPlayerWantToPlayAnotherRound());
        }

        public void PlayTurn()
        {
            ConsoleIO.DisplayWhoseTurn(CurrentPlayer.Name, CurrentPlayer.Symbol);
            if (CurrentPlayer.IsComputer)
            {
                PlayComputerTurn();
            }
            else // Human player's turn
            {
                ApplyHumanPlayerTurn();
            }
        }

        public void ApplyHumanPlayerTurn()
        {
            bool isMoveApplied = false;
            do
            {
                ConsoleIO.GetMoveFromPlayer(Board.BoardSize, out int row, out int col);
                if (row == Quit || col == Quit)
                {
                    IsRoundOver = true;
                    CurrentPlayer.Forfeited = true;
                    UpdatePointsAfterPlayerForfeits();
                }
                else if (Board.GetCellSymbol(row - 1, col - 1) != Empty)
                {
                    ConsoleIO.DisplayCellIsOccupiedMsg();
                }
                else
                {
                    ApplyMove(row - 1, col - 1);
                    isMoveApplied = true;
                }
            }
            while (!isMoveApplied && !IsRoundOver);
        }

        public void UpdatePointsAfterPlayerForfeits()
        {
            if (Player1 == CurrentPlayer)
            {
                Player2.Score++;
            }
            else
            {
                Player1.Score++;
            }
        }

        public void DisplayTheFinalBoardAndSummary()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            ConsoleIO.DisplayBoard(Board);
            ConsoleIO.DisplaySummary(this);
        }

        public void ApplyMove(int i_X, int i_Y)
        {
            Board.SetCellSymbol(i_X, i_Y, CurrentPlayer.Symbol);

            CheckForWinner();
            if (!IsRoundOver)
            {
                switchPlayer();
                if (CurrentPlayer.IsComputer)
                {
                    PlayComputerTurn();
                }
            }
        }

        public void PlayComputerTurn()
        {
            Random rnd = new Random();

            // Loop until a free cell is found
            while (true)
            {
                int row = rnd.Next(Board.BoardSize);
                int col = rnd.Next(Board.BoardSize);

                // Check if the cell is free
                if (Board.GetCellSymbol(row, col) == ' ')
                {
                    ApplyMove(row, col);
                    return;
                }
            }
        }


        private void switchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
        }

        public void CheckForWinner()
        {
            if (checkRowsForWinner() || checkColumnsForWinner() || checkDiagonalsForWinner())
            {
                IsRoundOver = true;
                switchPlayer();
                Winner = CurrentPlayer;
                Winner.Score++;
            }
            else if (checkForTie())
            {
                IsRoundOver = true;
                Winner = null;
            }
        }

        private bool checkRowsForWinner()
        {
            bool isWinner = false;
            for (int i = 0; i < Board.BoardSize; i++)
            {
                bool isLosingRow = true;
                for (int j = 0; j < Board.BoardSize; j++)
                {
                    if (Board.GetCellSymbol(i, j) != CurrentPlayer.Symbol)
                    {
                        isLosingRow = false;
                        break;
                    }
                }
                if (isLosingRow)
                {
                    isWinner = true;
                    break;
                }
            }
            return isWinner;
        }

        private bool checkColumnsForWinner()
        {
            bool isWinner = false;
            for (int j = 0; j < Board.BoardSize; j++)
            {
                bool isLosingColumn = true;
                for (int i = 0; i < Board.BoardSize; i++)
                {
                    if (Board.GetCellSymbol(i, j) != CurrentPlayer.Symbol)
                    {
                        isLosingColumn = false;
                        break;
                    }
                }
                if (isLosingColumn)
                {
                    isWinner = true;
                    break;
                }
            }
            return isWinner;
        }


        private bool checkDiagonalsForWinner()
        {
            bool isLosingDiagonal1 = true;
            bool isLosingDiagonal2 = true;
            for (int i = 0, j = 0; i < Board.BoardSize; i++, j++)
            {
                if (Board.GetCellSymbol(i, j) != CurrentPlayer.Symbol)
                {
                    isLosingDiagonal1 = false;
                }
                if (Board.GetCellSymbol(i, Board.BoardSize - 1 - j) != CurrentPlayer.Symbol)
                {
                    isLosingDiagonal2 = false;
                }
            }
            return isLosingDiagonal1 || isLosingDiagonal2;
        }

        private bool checkForTie()
        {
            return Board.IsBoardFull();
        }

        public void SetGameForNewRound()
        {
            CurrentPlayer.Forfeited = false;
            CurrentPlayer = (NumOfRounds) % 2 == 0 ? Player1 : Player2;
            Board = new Board(Board.BoardSize);
            IsRoundOver = false;
            Winner = null;
        }
    }


}
