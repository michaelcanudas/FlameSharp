namespace FlameSharp.Components
{
    public class Symbol : Token
    {
        public static string[] Patten = { "=", ":", ";" };

        public Symbol(string value) : base(value) { }
    }
}
