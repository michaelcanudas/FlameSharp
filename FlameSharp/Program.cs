using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FlameSharp.Components;
using LLVMSharp;

namespace FlameSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Handle(Lexer.Handle("var: i32 x = 10;"));

            LLVM.DumpModule(Parser.Module);
        }
    }

    class Lexer
    {
        public static List<Token> Handle(string source)
        {
            List<Token> tokens = new List<Token>();

            bool lexing = true;
            int index = 0;
            while (lexing)
            {
                switch (true)
                {
                    case true when Regex.IsMatch(source[index..(source.Length)], "var"):
                        string value = Regex.Match(source[index..(source.Length)], "var").Value;
                        tokens.Add(new Keyword(value));

                        index += value.Length;
                        break;
                    default:
                        throw new Exception("error");
                }
            }

            /*
            return new List<Token>()
            {
                new Keyword("var"),
                new Symbol(":"),
                new Type("i32"),
                new Identifier("x"),
                new Symbol("="),
                new Literal("10"),
                new Symbol(";")
            };*/
        }
    }

    class Parser
    {
        public static LLVMModuleRef Module { get; set; }
        public static LLVMBuilderRef Builder { get; set; }

        public static void Handle(List<Token> tokens)
        {
            Module = LLVM.ModuleCreateWithName("");
            Builder = LLVM.CreateBuilder();

            LLVMValueRef main = LLVM.AddFunction(Module, "main", LLVM.FunctionType(LLVMTypeRef.VoidType(), new LLVMTypeRef[] { }, false));
            LLVM.PositionBuilderAtEnd(Builder, main.AppendBasicBlock("entry"));

            for (int i = 0; i < tokens.Count; i++)
            {
                // eventually need to support types, keywords, identifiers, operators (everything except literal and symbol)
                if (!(tokens[i] is Keyword)) continue;

                (tokens[i] as Keyword).Handle(tokens, i);
            }
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
