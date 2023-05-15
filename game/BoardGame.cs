using System.Collections.Generic;

namespace game
{
    internal class BoardGame
    {
        public const char k_Empty = ' ';
        public const int k_MinSizeOfBoard = 3;
        public const int k_MaxSizeOfBoard = 9;
        public const int k_MinValOfDimension = 1;
        public Cell[,] Cells { get; set; }
        public List<Cell> EmptyCells;
        //public Cell FirstEmptyCell;
        public int BoardSize { get; set; }
        public BoardGame(int i_BoardSize)
        {
            BoardSize = i_BoardSize;
            Cells = new Cell[i_BoardSize, i_BoardSize];
            EmptyCells = new List<Cell>();
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    Cells[i, j] = new Cell(i, j);
                    EmptyCells.Add(Cells[i, j]);
                }
            }
        }

        internal void SetCellSymbol(int i_X, int i_Y, char i_Symbol)
        {
            Cells[i_X, i_Y].Symbol = i_Symbol;
        }

        internal char GetCellSymbol(int i_X, int i_Y)
        {
            return Cells[i_X, i_Y].Symbol;
        }

        internal bool IsBoardFull() ///////////////////// if the lst empty
        {
            bool isEmpty = true;

            foreach(Cell cell in Cells)
            {
                if(cell.IsCellEmpty())
                {
                    isEmpty = false;
                }
            }

            return isEmpty;
        }

        internal bool IsCellOnBoardEmpty(int i_Row, int i_Col)
        {
            return GetCellSymbol(i_Row, i_Col) == k_Empty;
        }

        internal void CleanCell(int i_Row, int i_Col)
        {
            SetCellSymbol(i_Row, i_Col, k_Empty);
        }
    }
}
