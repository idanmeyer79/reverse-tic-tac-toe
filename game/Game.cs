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

        // Ctor
        public Game(Player i_Player1, Player i_Player2, int i_BoardSize)
        {
            Player1 = i_Player1;
            Player2 = i_Player2;
            CurrentPlayer = new Random().Next(2) == 0 ? i_Player1 : i_Player2; // randomly choose first player
            Board = new Board(i_BoardSize);
            IsGameOver = false;
            Winner = null;
        }

        public void PlayTurn(int i_X, int i_Y)
        {

            if (Board.GetCellSymbol(i_X, i_Y) != ' ')
            {
                Console.WriteLine("That square is already occupied. press any key to try again");
                Console.ReadKey();
                return;
            }

            Board.SetCellSymbol(i_X, i_Y, CurrentPlayer.Symbol);

            CheckForWinner();
            if (!IsGameOver)
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
                IsGameOver = true;
                switchPlayer();
                Winner = CurrentPlayer;
                Winner.Score++;
            }
            else if (checkForTie())
            {
                IsGameOver = true;
                Winner = null;
            }
        }

        private bool checkRowsForWinner()
        {
            bool isWinner = false;
            for (int i = 0; i < Board.BoardSize; i++)
            {
                bool isWinningRow = true;
                for (int j = 0; j < Board.BoardSize; j++)
                {
                    if (Board.GetCellSymbol(i, j) != CurrentPlayer.Symbol)
                    {
                        isWinningRow = false;
                        break;
                    }
                }
                if (isWinningRow)
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
                bool isWinningColumn = true;
                for (int i = 0; i < Board.BoardSize; i++)
                {
                    if (Board.GetCellSymbol(i, j) != CurrentPlayer.Symbol)
                    {
                        isWinningColumn = false;
                        break;
                    }
                }
                if (isWinningColumn)
                {
                    isWinner = true;
                    break;
                }
            }
            return isWinner;
        }


        private bool checkDiagonalsForWinner()
        {
            bool isWinningDiagonal1 = true;
            bool isWinningDiagonal2 = true;
            for (int i = 0, j = 0; i < Board.BoardSize; i++, j++)
            {
                if (Board.GetCellSymbol(i, j) != CurrentPlayer.Symbol)
                {
                    isWinningDiagonal1 = false;
                }
                if (Board.GetCellSymbol(i, Board.BoardSize - 1 - j) != CurrentPlayer.Symbol)
                {
                    isWinningDiagonal2 = false;
                }
            }
            return isWinningDiagonal1 || isWinningDiagonal2;
        }

        private bool checkForTie()
        {
            return Board.IsBoardFull();
        }


    }


}
