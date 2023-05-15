namespace game
{
    internal class Cell
    {
        public char Symbol { get; set; }
        public int XDimension { get; set; }
        public int YDimension { get; set; }

        public Cell(int i_XDimension, int i_YDimension)
        {
            Symbol = ' ';
            XDimension = i_XDimension;
            YDimension = i_YDimension;
        }

        internal bool IsCellEmpty()
        {
            return Symbol == ' ';
        }
    }

}
