namespace game
{
    internal class Player
    {
        private string m_name;

        private char m_Symbol;
        public bool IsComputer { get; set; }
        public int Score { get; set; }

        public Player(string i_Name, char i_Symbol)
        {
            Name = i_Name;
            Symbol = i_Symbol;
            IsComputer = false;
            Score = 0;
        }

        public string Name { get; set; }

        public char Symbol {get; set; }
    }

}
