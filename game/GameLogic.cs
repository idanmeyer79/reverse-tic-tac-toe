using System;

namespace game
{
    internal class GameLogic
    {
        public const int FirstRound = 0;
        public const char Empty = ' ';
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer { get; set;}
        public Player Winner { get; set; }
        public BoardGame Board { get; set; }
        public int NumOfRounds { get; set; }
        public bool IsRoundOver { get; set; }

        // Ctor
        public GameLogic(Player i_Player1, Player i_Player2, int i_BoardSize)
        {
            Player1 = i_Player1;
            Player2 = i_Player2;
            CurrentPlayer = (NumOfRounds) % 2 == 0 ? Player1 : Player2;
            Board = new BoardGame(i_BoardSize);
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

        public void ApplyMove(int i_X, int i_Y)
        {
            Board.SetCellSymbol(i_X, i_Y, CurrentPlayer.Symbol);

            CheckForWinner();
            if (!IsRoundOver)
            {
                switchPlayer();
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

        public void ApplyComputerPlayerTurn()
        {

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
            if (NumOfRounds++ != FirstRound)
            {
                CurrentPlayer.Forfeited = false;
                CurrentPlayer = (NumOfRounds) % 2 == 0 ? Player1 : Player2;
                Board = new BoardGame(Board.BoardSize);
                IsRoundOver = false;
                Winner = null;
            }        
        }

        public void PrepareGameForQuitting()
        {
            IsRoundOver = true;
            CurrentPlayer.Forfeited = true;
            UpdatePointsAfterPlayerForfeits();
        }

        public bool isCellOnBoardNotEmpty(int i_Row, int i_Col)
        {
            return Board.GetCellSymbol(i_Row - 1, i_Col - 1) != Empty;
        }
    }
}
