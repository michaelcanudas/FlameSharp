namespace FlameSharp.Tokens
{
    public class Token
    {
        public int Position { get; set; }
        public string Value { get; set; }
        public TokenType Type { get; set; }

        public Token(int position, string value, TokenType type)
        {
            Position = position;
            Value = value;
            Type = type;
        }

        public enum TokenType
        {
            Keyword,
            Type,
            Symbol,
            Operator,
            Literal,
            Identifier
        }
    }
}
