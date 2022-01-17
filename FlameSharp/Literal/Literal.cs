namespace FlameSharp.Components
{
    class Literal : Token
    {
        public static string Pattern = "[0-9]+";

        public Literal(int position, string value) : base(position, value) { }

        public ulong Handle()
        {
            return ulong.Parse(Value);
        }
    }
}
