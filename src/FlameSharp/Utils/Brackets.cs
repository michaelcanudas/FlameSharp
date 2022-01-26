using System;
using System.Collections.Generic;
using FlameSharp.Tokens;

namespace FlameSharp.Utils
{
    public class Brackets
    {
        public static Token FindClosingBracket(List<Token> tokens, int open)
        {
            Stack<Token> brackets = new Stack<Token>();
            for (int i = open; i < tokens.Count; i++)
            {
                if (tokens[i].Type == Token.TokenType.Symbol && tokens[i].Value == "{")
                    brackets.Push(tokens[i]);
                else if (tokens[i].Type == Token.TokenType.Symbol && tokens[i].Value == "}")
                    brackets.Pop();

                if (brackets.Count == 0)
                    return tokens[i];
            }

            throw new Exception("error");
        }

        /*
        if {

        }

        if {
            if {

            }
        }

        if {
            
        }
        */
    }
}
