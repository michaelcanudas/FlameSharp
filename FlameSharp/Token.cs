namespace FlameSharp
{
    public class Token
    {
        public int Position { get; set; }
        public string Value { get; set; }

        public Token(int position, string value)
        {
            Position = position;
            Value = value;
        }
    }
}
