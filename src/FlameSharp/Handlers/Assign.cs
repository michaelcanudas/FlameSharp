using System;
using System.Collections.Generic;
using FlameSharp.Stacks;
using FlameSharp.Tokens;
using FlameSharp.Parsers;
using LLVMSharp;

namespace FlameSharp.Handlers
{
    public class Assign
    {
        public static void Handle(List<Token> tokens, ref int i)
        {
            Token id = tokens[i].Type == Token.TokenType.Identifier ? tokens[i] : throw new Exception("error");
            Token op = tokens[i + 1].Type == Token.TokenType.Operator && tokens[i + 1].Value == "=" ? tokens[i + 1] : throw new Exception("error");
            Token type = tokens[i + 2].Type == Token.TokenType.Type ? tokens[i + 2] : throw new Exception("error");
            i += 2;

            ValueParser.Parse(ValueParser.Generate(tokens, ref i));
            (LLVMValueRef value, LLVMTypeKind type) var = ValueStack.Pop();

            LLVM.BuildStore(Parser.Builder, var.value, ValueStack.Get(id.Value).Item1);
            // check if variable was declared outside of current conditional
            // if so, use phi instead of normal assign

            /*
            LLVM.BuildPhi(Parser.Builder, LLVMTypeRef.Int32Type(), "phi")
                .AddIncoming(
                    new LLVMValueRef[] { var.value },
                    new LLVMBasicBlockRef[] { FuncStack.Get("main").GetBasicBlocks()[1] },
                    2);
            */
            ValueStack.Put(id.Value, var);
        }
    }
}
