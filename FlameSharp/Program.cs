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
            Lexer.Handle("i32: x = 10;");
            //Parser.Handle(tokens);
            //Parser.Handle(Lexer.Handle("var: i32 x = 10;"));

            //LLVM.DumpModule(Parser.Module);
        }
    }

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
                            return (reversed[i] as Operator).Handle(lhs, rhs, Operator.HandleType.Signed);
                        }
                    case true when reversed[i] is Operator && reversed[i].Value == "-":
                        {
                            List<Token> lhs = new List<Token>(reversed.ToArray()[(i + 1)..(reversed.Count)]);
                            List<Token> rhs = new List<Token>(reversed.ToArray()[0..i]);
                            return (reversed[i] as Operator).Handle(lhs, rhs, Operator.HandleType.Signed);
                        }
                    case true when reversed[i] is Literal:
                        return LLVM.ConstInt(LLVMTypeRef.Int32Type(), (reversed[i] as Literal).Handle(), true);
                }
            }

            throw new Exception("error");
        }
    }
}
