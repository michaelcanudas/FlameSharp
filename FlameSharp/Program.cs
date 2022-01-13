using System;
using System.Collections.Generic;
using System.Linq;
using LLVMSharp;

namespace FlameSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Token> tokens = Lexer.Handle("let: i32 x = 10;");
            SGenerator.Handle(tokens);
        }
    }

    class Lexer
    {
        public static List<Token> Handle(string source)
        {
            // temporary
            return new List<Token>()
            {
                new Keyword("let"),
                new Symbol(":"),
                new Type("i32"),
                new Identifier("x"),
                new Operator("="),
                new Literal("10"),
                new Symbol(";")
            };
        }
    }

    class SGenerator
    {
        public static void Handle(List<Token> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                tokens[i].Handle(tokens, ref i);
            }
        }
    }

    class EGenerator
    {
        public static LLVMValueRef Handle(List<Token> tokens)
        {
            return LLVM.ConstInt(LLVMTypeRef.Int32Type(), 10, true);
        }
    }

    abstract class Token
    {
        public string Value { get; set; }

        public Token(string value)
        {
            Value = value;
        }

        public abstract void Handle(List<Token> tokens, ref int i);
    }

    class Keyword : Token
    {
        public Keyword(string value) : base(value) { }

        public override void Handle(List<Token> tokens, ref int i)
        {
            if (!(tokens[i] is Keyword)) throw new Exception("error");

            switch (tokens[i].Value)
            {
                case "let":
                    HandleLet(tokens, ref i);
                    break;
            }
        }

        private void HandleLet(List<Token> tokens, ref int i)
        {
            int _i = i;
            Type type = tokens[i + 2] is Type ? tokens[i + 2] as Type : throw new Exception("error");
            Identifier id = tokens[i + 3] is Identifier ? tokens[i + 3] as Identifier : throw new Exception("error");
            Symbol end = tokens.Where((x, j) => x.Value == ";" && j > _i).First() is Symbol ? tokens.Where((x, j) => x.Value == ";" && j > _i).First() as Symbol : throw new Exception("error");
            LLVMValueRef value = EGenerator.Handle(new List<Token>(tokens.ToArray()[(i + 5)..(tokens.IndexOf(end))]));

            LLVMBuilderRef b = LLVM.CreateBuilder();
            LLVMValueRef v = LLVM.BuildAlloca(b, type.ToLLVM(), id.Value); // use stack layer in future
            LLVM.BuildStore(b, value, v);
        }
    }

    class Type : Token
    {
        public static string[] Patten = { "i32" };

        public Type(string value) : base(value) { }

        public override void Handle(List<Token> tokens, ref int i) { }

        public LLVMTypeRef ToLLVM()
        {
            return Value switch
            {
                "i32" => LLVMTypeRef.Int32Type()
            };
        }
    }

    class Symbol : Token
    {
        public static string[] Patten = { ":", ";" };

        public Symbol(string value) : base(value) { }

        public override void Handle(List<Token> tokens, ref int i)
        {

        }
    }

    class Operator : Token
    {
        public static string[] Patten = { "=" };

        public Operator(string value) : base(value) { }

        public override void Handle(List<Token> tokens, ref int i)
        {

        }
    }

    class Literal : Token
    {
        public static string[] Patten = { "[0-9]+" };

        public Literal(string value) : base(value) { }

        public override void Handle(List<Token> tokens, ref int i)
        {

        }
    }

    class Identifier : Token
    {
        public static string[] Pattern = { "[a-z][a-zA-Z0-9]*" };

        public Identifier(string value) : base(value) { }

        public override void Handle(List<Token> tokens, ref int i)
        {

        }
    }
}
