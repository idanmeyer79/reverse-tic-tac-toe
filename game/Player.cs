namespace game
{
    internal class Player
    {
        public string Name { get; set; }
        public char Symbol { get; set; }
        public bool IsComputer { get; set; }
        public int Score { get; set; }

        public Player(string i_Name, char i_Symbol)
        {
            Name = i_Name;
            Symbol = i_Symbol;
            IsComputer = false;
            Score = 0;
        }
    }

}
