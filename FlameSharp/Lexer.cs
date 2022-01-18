using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FlameSharp.Components;
using Type = FlameSharp.Components.Type;

namespace FlameSharp
{
    public class Lexer
    {
        public static List<Token> Handle(string source)
        {
            List<Token> tokens = new List<Token>();
            Dictionary<string, Func<int, string, Token>> handlers = new Dictionary<string, Func<int, string, Token>>()
            {
                { Keyword.Pattern, (i, j) => new Keyword(i, j) },
                { Type.Pattern, (i, j) => new Type(i, j) },
                { Symbol.Pattern, (i, j) => new Symbol(i, j) },
                { Operator.Pattern, (i, j) => new Operator(i, j) },
                { Literal.Pattern, (i, j) => new Literal(i, j) },
                { Identifier.Pattern, (i, j) => new Identifier(i, j) }
            };
            List<string> previousHandlers = new List<string>();

            foreach (var x in handlers)
            {
                int index = 0;
                string check = source[index..(source.Length)];

                while (Regex.IsMatch(check, x.Key))
                {
                    Match match = Regex.Match(source[index..(source.Length)], x.Key);
                    index += match.Index + match.Value.Length;
                    check = source[index..(source.Length)];

                    if (previousHandlers.Where(y => Regex.IsMatch(match.Value, y)).Count() != 0) continue;
                    else tokens.Add(x.Value(index - match.Value.Length, match.Value));
                }

                previousHandlers.Add(x.Key);
            }

            tokens = tokens.OrderBy(x => x.Position).ToList();
            return tokens;
        }
    }
}
