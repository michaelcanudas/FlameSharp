namespace FlameSharp.Components
{
    public class Symbol : Token
    {
        public static string Pattern = @":|;";

        public Symbol(int position, string value) : base(position, value) { }
    }
}
