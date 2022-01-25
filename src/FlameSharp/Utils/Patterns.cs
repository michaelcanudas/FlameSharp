using System;
using System.Collections.Generic;
using FlameSharp.Tokens;

namespace FlameSharp.Utils
{
    public class Patterns
    {
        public static bool ValidatePattern(List<Token> tokens, PatternType[] types)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if (types[i] != Block && types[i] != Value)
                    if ((int)tokens[i].Type != (int)types[i]) return false;

                // pass block to block parser

                // parse value to value parser
            }

            return true;
        }

        public enum PatternType
        {
            Keyword,
            Type,
            Symbol,
            Operator,
            Literal,
            Identifier,
            Block,
            Value
        }
    }
}
