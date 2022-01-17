using System;
using System.Collections.Generic;

namespace FlameSharp.Components
{
    class Identifier : Token
    {
        public static string Pattern = "[a-z][a-zA-Z0-9]*";

        public Identifier(int position, string value) : base(position, value) { }

        public void Handle(List<Token> tokens, ref int i)
        {
            switch (Value)
            {
                default:
                    throw new Exception("error");
            }
        }
    }
}
