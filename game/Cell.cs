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

        public bool IsCellEmpty()
        {
            return Symbol == ' ';
        }
        //    public override bool Equals(object obj)
        //    {
        //        if (obj == null || GetType() != obj.GetType())
        //            return false;

        //        var other = (Cell)obj;
        //        return XDimension == other.XDimension && YDimension == other.YDimension;
        //    }

        //    public override int GetHashCode()
        //    {
        //        return XDimension.GetHashCode() ^ YDimension.GetHashCode();
        //    }
        //    public static bool operator ==(Cell c1, Cell c2)
        //    {
        //        if (ReferenceEquals(c1, c2))
        //        {
        //            return true;
        //        }

        //        if ((object)c1 == null || (object)c2 == null)
        //        {
        //            return false;
        //        }

        //        return c1.XDimension == c2.XDimension && c1.YDimension == c2.YDimension;
        //    }

        //    public static bool operator !=(Cell c1, Cell c2)
        //    {
        //        return !(c1 == c2);
        //    }
    }

}
