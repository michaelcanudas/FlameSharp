namespace FlameSharp.Components
{
    class Identifier : Token
    {
        public static string[] Pattern = { "[a-z][a-zA-Z0-9]*" };

        public Identifier(string value) : base(value) { }

        /*public void Handle(List<Token> tokens, ref int i)
        {

        }*/
    }
}
