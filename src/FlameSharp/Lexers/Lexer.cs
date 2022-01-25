using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FlameSharp.Tokens;

namespace FlameSharp.Lexers
{
    public class Lexer
    {
        public static List<Token> Handle(string source)
        {
            // fix double tokenize with -> and -
            // also check all handlers to make sure all tokens are being checked

            List<Token> tokens = new List<Token>();
            Dictionary<string, Func<int, string, Token>> handlers = new Dictionary<string, Func<int, string, Token>>()
            {
                { @"let|if", (i, j) => new Token(i, j, Token.TokenType.Keyword) },
                { @"i32", (i, j) => new Token(i, j, Token.TokenType.Type) },
                { @"->|{|}|;", (i, j) => new Token(i, j, Token.TokenType.Symbol) },
                { @"==|!=|=|\+|-|\*|\/|%", (i, j) => new Token(i, j, Token.TokenType.Operator) },
                { @"\b[0-9]+\b", (i, j) => new Token(i, j, Token.TokenType.Literal) },
                { @"[a-z][a-zA-Z0-9]*", (i, j) => new Token(i, j, Token.TokenType.Identifier) }
            };
            List<string> previousHandlers = new List<string>();

            foreach (KeyValuePair<string, Func<int, string, Token>> x in handlers)
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
