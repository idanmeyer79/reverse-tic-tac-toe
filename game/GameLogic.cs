using System;
using System.Collections.Generic;

namespace game
{
    internal class GameLogic
    {
        //public enum eGameState
        //{
        //    X = 1,
        //    O = -1,
        //    TIE = 0,
        //}
        public const int k_FirstRound = 0;
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer { get; set; }
        public Player Winner { get; set; }
        public BoardGame Board { get; set; }
        public int NumOfRounds { get; set; }
        public bool IsRoundOver { get; set; }

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
            Board.EmptyCells.Remove(Board.Cells[i_X, i_Y]);
            Board.SetCellSymbol(i_X, i_Y, CurrentPlayer.Symbol);
            CheckGameStatusAndUpdate();
            if (!IsRoundOver)
            {
                switchPlayer();
            }
        }

        public void PlayComputerTurn()
        {
            Random rnd = new Random();
            bool isRandomCellHasFound = false;

            while (!isRandomCellHasFound)
            {
                int row = rnd.Next(Board.BoardSize);
                int col = rnd.Next(Board.BoardSize);

                if (Board.IsCellOnBoardEmpty(row, col))
                {
                    Board.SetCellSymbol(row, col, CurrentPlayer.Symbol);
                    bool isThereWinner = CheckForWinner();
                    Board.CleanCell(row, col);
                    if (!isThereWinner)
                    {
                        ApplyMove(row, col);
                        isRandomCellHasFound = true;
                    } 
                }
            }
        }

        public void ApplyComputerPlayerTurn()
        {
            if(Board.EmptyCells.Count > 8)
            {
                PlayComputerTurn();
            }
            else
            {
                NextMove(out int row, out int col, 0);
                ApplyMove(row, col);
            }

        }

        public int MinimaxAlgorithm(int i_Depth)
        {
            int scoreOfAlgorithm = 0;

            if (CheckForWinner()) // if there is a winner while its current player's turn, then it implies that the current player has lost.
            {
                if (CurrentPlayer.IsComputer)
                {
                    scoreOfAlgorithm = -10; // שיקח מהלך שבו הוא מנצח מהר יותר
                }
                else
                {
                    scoreOfAlgorithm = 10;
                }
            }
            else if (!Board.IsBoardFull())
            {
                switchPlayer();
                scoreOfAlgorithm = NextMove(out int row, out int col, i_Depth + 1);
                switchPlayer();
            }

            return scoreOfAlgorithm;
        }
        public int NextMove(out int io_Row, out int io_Col, int i_Depth)
        {
            int bestScore;
            if (CurrentPlayer.IsComputer)
            {
                bestScore = Int32.MinValue;
            }
            else
            {
                bestScore = Int32.MaxValue;
            }

            io_Col = 0;
            io_Row = 0;
            for (int i = 0; i < Board.EmptyCells.Count; i++)
            {
                Cell currCell = Board.EmptyCells[i];
                Board.EmptyCells.Remove(currCell);
                Board.SetCellSymbol(currCell.XDimension, currCell.YDimension, CurrentPlayer.Symbol);
                int scoreOfAlgorithm = MinimaxAlgorithm(i_Depth);
                Board.CleanCell(currCell.XDimension, currCell.YDimension);
                Board.EmptyCells.Insert(0, currCell);

                if ((CurrentPlayer.IsComputer && scoreOfAlgorithm > bestScore) ||
                    (!CurrentPlayer.IsComputer && scoreOfAlgorithm < bestScore))
                {
                    bestScore = scoreOfAlgorithm;
                    // bestMove
                    io_Row = currCell.XDimension;
                    io_Col = currCell.YDimension;


                }

            }
            return bestScore;

        }

        private void switchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
        }

        public void CheckGameStatusAndUpdate()
        {
            if (CheckForWinner())
            {
                IsRoundOver = true;
                switchPlayer();
                Winner = CurrentPlayer;
                Winner.Score++;
            }
            else if (Board.IsBoardFull())
            {
                IsRoundOver = true;
            }
        }

        public bool CheckForWinner()
        {
            bool isThereWinner = false;

            if (checkRowsForWinner() || checkColumnsForWinner() || checkDiagonalsForWinner())
            {
                isThereWinner = true;
            }

            return isThereWinner;
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

        public void SetGameForNewRound()
        {
            if (NumOfRounds++ != k_FirstRound)
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
    }
}
