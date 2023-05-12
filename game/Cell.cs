namespace game
{
    internal class Cell
    {
        // Data Member
        public char Symbol { get; set; }
        
        // Constructor
        public Cell()
        {
            Symbol = ' ';
        }

        // Method
        public bool IsCellEmpty()
        {
            return Symbol == ' ';
        }
    }

}
