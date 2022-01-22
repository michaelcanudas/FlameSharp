using System;
using System.Linq;
using System.Collections.Generic;
using FlameSharp.Components;
using LLVMSharp;
using System.IO;

namespace FlameSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Token> tokens = Lexer.Handle(File.ReadAllText("./Example/test.flm"));
            Parser.Handle(tokens);

            LLVM.DumpModule(Parser.Module);
        }
    }

    // NEXT THING TO WORK ON IS EXPRESSION PARSER
    class ExpressionParser
    {
        public static (LLVMValueRef, LLVMTypeKind) Handle(IList<Token> tokens)
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
                }
            }

            for (int i = 0; i < reversed.Count; i++)
            {
                if (reversed[i] is Operator op)
                {
                    switch (op.Value)
                    {
                        case "*":
                            {
                                List<Token> lhs = new List<Token>(reversed.ToArray()[(i + 1)..(reversed.Count)]);
                                List<Token> rhs = new List<Token>(reversed.ToArray()[0..i]);

                                return op.ParseBinary(lhs, rhs, Operator.HandleType.Signed);
                            }
                        case "/":
                            {
                                List<Token> lhs = new List<Token>(reversed.ToArray()[(i + 1)..(reversed.Count)]);
                                List<Token> rhs = new List<Token>(reversed.ToArray()[0..i]);

                                return op.ParseBinary(lhs, rhs, Operator.HandleType.Signed);
                            }
                    }
                }
            }

            for (int i = 0; i < reversed.Count; i++)
            {
                switch (true)
                {
                    case true when reversed[i] is Identifier:
                        if (reversed.Count() > 1) throw new Exception("error");

                        return (reversed[i] as Identifier).Parse();
                }
            }

            for (int i = 0; i < reversed.Count; i++)
            {
                switch (true)
                {
                    case true when reversed[i] is Literal:
                        if (reversed.Count() > 1) throw new Exception("error");

                        return (reversed[i] as Literal).Parse();
                }
            }

            throw new Exception("error");
        }
    }

    class Stack
    {
        static Dictionary<string, (LLVMValueRef, LLVMTypeKind)> values = new Dictionary<string, (LLVMValueRef, LLVMTypeKind)>();
        public static void Add(string id, (LLVMValueRef, LLVMTypeKind) value)
        {
            values.Add(id, value);
        }

        public static (LLVMValueRef, LLVMTypeKind) Get(string id)
        {
            return values[id];
        }
    }


    // SLIGHT REVAMP
    // generic token with token type, parse and handle function
    // lexer uses generic token and sets type
    // parser uses generic token and uses parse and handle functions
    // components are in new folder, containing statements such as "if"
    // utils are brought to a new folder, handling things like the value stack
}
