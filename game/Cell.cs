namespace game
{
    internal class Cell
    {
        public char Symbol { get; set; }
        
        public Cell()
        {
            Symbol = ' ';
        }

        public bool IsCellEmpty()
        {
            return Symbol == ' ';
        }
    }

}
