using System;
using System.Collections.Generic;

namespace FlameSharp.Components
{
    public partial class Keyword : Token
    {
        public static string Pattern = "var";

        public Keyword(int position, string value) : base(position, value) { }

        public void Handle(List<Token> tokens, ref int i)
        {
            switch (Value)
            {
                case "var":
                    HandleVar(tokens, ref i);
                    break;
                default:
                    throw new Exception("error");
            }
        }
    }
}
