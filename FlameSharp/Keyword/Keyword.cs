using System;
using System.Collections.Generic;

namespace FlameSharp.Components
{
    public class Keyword : Token
    {
        public static string Pattern = @"KEYWORD";

        public Keyword(int position, string value) : base(position, value) { }

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
