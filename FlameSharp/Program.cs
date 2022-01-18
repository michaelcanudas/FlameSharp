using System;
using System.Linq;
using System.Collections.Generic;
using FlameSharp.Components;
using LLVMSharp;

namespace FlameSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Token> tokens = Lexer.Handle("");
            Parser.Handle(tokens);

            LLVM.DumpModule(Parser.Module);
        }
    }

    // NEXT THING TO WORK ON IS EXPRESSION PARSER
    class ExpressionParser
    {
        public static LLVMValueRef Handle(IList<Token> tokens)
        {
            List<Token> reversed = tokens.Reverse().ToList();

            for (int i = 0; i < reversed.Count; i++)
            {
                switch (true)
                {
                    case true when reversed[i] is Operator && reversed[i].Value == "+":
                        {
                            List<Token> lhs = new List<Token>(reversed.ToArray()[(i + 1)..(reversed.Count)]);
                            List<Token> rhs = new List<Token>(reversed.ToArray()[0..i]);
                            return (reversed[i] as Operator).ParseBinary(lhs, rhs, Operator.HandleType.Signed);
                        }
                    case true when reversed[i] is Operator && reversed[i].Value == "-":
                        {
                            List<Token> lhs = new List<Token>(reversed.ToArray()[(i + 1)..(reversed.Count)]);
                            List<Token> rhs = new List<Token>(reversed.ToArray()[0..i]);
                            return (reversed[i] as Operator).ParseBinary(lhs, rhs, Operator.HandleType.Signed);
                        }
                    case true when reversed[i] is Identifier:
                        return (reversed[i] as Identifier).Parse();
                    case true when reversed[i] is Literal:
                        return (reversed[i] as Literal).Parse();
                }
            }

            throw new Exception("error");
        }
    }
}
