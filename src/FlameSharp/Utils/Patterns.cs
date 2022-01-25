using System;
using System.Collections.Generic;
using FlameSharp.Tokens;

namespace FlameSharp.Utils
{
    public class Patterns
    {
        public static bool ValidatePattern(List<Token> tokens, int[] types)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type != (Token.TokenType)types[i]) return false;
            }

            return true;
        }
    }
}
