using System;

namespace game
{
    internal class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer { get; set;}
        public Player Winner { get; set; }
        public Board Board { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsRoundOver { get; set; }

        // Ctor
        public Game(Player i_Player1, Player i_Player2, int i_BoardSize)
        {
            Player1 = i_Player1;
            Player2 = i_Player2;
            CurrentPlayer = new Random().Next(2) == 0 ? i_Player1 : i_Player2; // randomly choose first player
            Board = new Board(i_BoardSize);
            IsGameOver = false;
            IsRoundOver = false;
            Winner = null;
        }

        public void PlayTurn(int i_X, int i_Y)
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
                    PlayTurn(row, col);
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

        public void ResetGame()
        {
            CurrentPlayer = new Random().Next(2) == 0 ? Player1 : Player2; // randomly choose first player
            Board = new Board(Board.BoardSize);
            IsRoundOver = false;
            IsGameOver = false;
            Winner = null;
            Player1.Forfeited = false;
            Player2.Forfeited = false;
        }
    }


}
