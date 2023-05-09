﻿namespace game
{
    internal class Board
    {
        public Cell[,] Cells { get; set; }

        public int BoardSize { get; set; }

        public Board(int i_BoardSize)
        {
            BoardSize = i_BoardSize;
            Cells = new Cell[i_BoardSize, i_BoardSize];
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }
        }

        // Methods
        public void SetCellSymbol(int i_X, int i_Y, char i_Symbol)
        {
            Cells[i_X, i_Y].Symbol = i_Symbol;
        }

        public char GetCellSymbol(int i_X, int i_Y)
        {
            return Cells[i_X, i_Y].Symbol;
        }

        public bool IsBoardFull()
        {
            bool isEmpty = true;

            foreach(Cell cell in Cells)
            {
                if(cell.Symbol == ' ')
                {
                    isEmpty = false;
                }
            }

            return isEmpty;
        }
    }
}
