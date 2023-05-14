namespace game
{
    internal class Player
    {
        public string Name { get; set; }
        public char Symbol { get; set; }
        public bool IsComputer { get; set; }
        public int Score { get; set; }
        public bool Forfeited { get; set; }

        public Player(string i_Name, char i_Symbol)
        {
            Name = char.ToUpper(i_Name[0]) + i_Name.Substring(1);
            Symbol = i_Symbol;
        }
    }

}
