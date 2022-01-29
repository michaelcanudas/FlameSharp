using System;
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
            Token id = tokens[i + 1].Type == Token.TokenType.Identifier ? tokens[i + 1] : throw new Exception("error");
            Token op = tokens[i + 2].Type == Token.TokenType.Operator && tokens[i + 2].Value == "=" ? tokens[i + 2] : throw new Exception("error");
            Token type = tokens[i + 3].Type == Token.TokenType.Type ? tokens[i + 3] : throw new Exception("error");
            i += 3;

            ValueParser.Parse(ValueParser.Generate(tokens, ref i));
            (LLVMValueRef value, LLVMTypeKind type) var = ValueStack.Pop();

            LLVMValueRef ptr = LLVM.BuildAlloca(Parser.Builder, LLVM.Int32Type(), id.Value);
            LLVM.BuildStore(Parser.Builder, var.value, ptr);

            ValueStack.Push(var, id.Value);
        }
    }
}
