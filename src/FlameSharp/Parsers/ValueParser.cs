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

        public static List<Token> Generate(List<Token> tokens, ref int i)
        {
            int _i = i;

            Token start = tokens.Where((x, j) =>
                (x.Type == Token.TokenType.Operator ||
                x.Type == Token.TokenType.Identifier ||
                x.Type == Token.TokenType.Literal) &&
                j > _i
            ).First();
            int startIndex = tokens.IndexOf(start);

            Token end = tokens.Where((x, j) =>
                x.Type == Token.TokenType.Symbol &&
                j > startIndex
            ).First();
            int endIndex = tokens.IndexOf(end);
            
            // find way to throw error

            i = endIndex;

            return new List<Token>(tokens.ToArray()[startIndex..endIndex]);
        }
    }
}
