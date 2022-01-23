using System;
using System.Linq;
using System.Collections.Generic;
using FlameSharp.Tokens;
using FlameSharp.Handlers;

namespace FlameSharp.Parsers
{
    public class ValueParser
    {
        public static void Parse(IList<Token> tokens)
        {
            List<Token> reversed = tokens.Reverse().ToList();

            // cmp
            // add/sub
            // mul/div/mod
            // id
            // lit

            for (int i = 0; i < reversed.Count; i++)
            {
                List<Token> lhs = new List<Token>(reversed.ToArray()[(i + 1)..(reversed.Count)]);
                List<Token> rhs = new List<Token>(reversed.ToArray()[0..i]);
                if (reversed[i].Type == Token.TokenType.Operator)
                    switch (reversed[i].Value)
                    {
                        case "==":
                            Eq.Handle(lhs, rhs);
                            return;
                        case "!=":
                            Ne.Handle(lhs, rhs);
                            return;
                    }
            }

            for (int i = 0; i < reversed.Count; i++)
            {
                List<Token> lhs = new List<Token>(reversed.ToArray()[(i + 1)..(reversed.Count)]);
                List<Token> rhs = new List<Token>(reversed.ToArray()[0..i]);
                if (reversed[i].Type == Token.TokenType.Operator)
                    switch (reversed[i].Value)
                    {
                        case "+":
                            Add.Handle(lhs, rhs);
                            return;
                        case "-":
                            Sub.Handle(lhs, rhs);
                            return;
                    }
            }

            for (int i = 0; i < reversed.Count; i++)
            {
                List<Token> lhs = new List<Token>(reversed.ToArray()[(i + 1)..(reversed.Count)]);
                List<Token> rhs = new List<Token>(reversed.ToArray()[0..i]);
                if (reversed[i].Type == Token.TokenType.Operator)
                    switch (reversed[i].Value)
                    {
                        case "*":
                            Mul.Handle(lhs, rhs);
                            return;
                        case "/":
                            Div.Handle(lhs, rhs);
                            return;
                        case "%":
                            Rem.Handle(lhs, rhs);
                            return;
                    }
            }

            for (int i = 0; i < reversed.Count; i++)
            {
                if (reversed[i].Type == Token.TokenType.Identifier)
                {
                    Id.Handle(reversed[i].Value);
                    return;
                }
            }

            for (int i = 0; i < reversed.Count; i++)
            {
                if (reversed[i].Type == Token.TokenType.Literal)
                {
                    Lit.Handle(reversed[i].Value);
                    return;
                }
            }

            throw new Exception("error");
        }
    }
}
