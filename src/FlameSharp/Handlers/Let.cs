using System;
using System.Linq;
using System.Collections.Generic;
using FlameSharp.Stacks;
using FlameSharp.Tokens;
using FlameSharp.Parsers;
using LLVMSharp;

namespace FlameSharp.Handlers
{
    public class Let
    {
        public static void Handle(List<Token> tokens, ref int i)
        {
            int _i = i;

            if (tokens[i + 2].Type != Token.TokenType.Identifier) throw new Exception("error");
            if (tokens[i + 3].Type != Token.TokenType.Operator || !(tokens[i + 3].Value == "=")) throw new Exception("error");
            if (tokens[i + 4].Type != Token.TokenType.Type) throw new Exception("error");

            Token end = tokens.Where((x, j) => x.Value == ";" && x.Type == Token.TokenType.Symbol && j > _i).First();
            if (end == null) throw new Exception("error");

            ValueParser.Parse(new List<Token>(tokens.ToArray()[(i + 4)..tokens.IndexOf(end)]));
            (LLVMValueRef value, LLVMTypeKind type) var = ValueStack.Pop();

            LLVMValueRef ptr = LLVM.BuildAlloca(Parser.Builder, LLVM.Int32Type(), tokens[i + 2].Value);
            LLVM.BuildStore(Parser.Builder, var.value, ptr);

            ValueStack.Push(var, tokens[i + 2].Value);

            i = tokens.IndexOf(end);
        }
    }
}
