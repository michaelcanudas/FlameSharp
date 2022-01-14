namespace FlameSharp.Components
{
    class Literal : Token
    {
        public static string[] Patten = { "[0-9]+" };

        public Literal(string value) : base(value) { }

        public ulong Handle()
        {
            return ulong.Parse(Value);
        }
    }
}
