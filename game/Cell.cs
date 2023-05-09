namespace game
{
    class Cell
    {
        // Data Member
        public char Symbol { get; set; }
        

        // Constructor
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
