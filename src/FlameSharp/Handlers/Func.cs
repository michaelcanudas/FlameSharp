using System;
using System.Collections.Generic;
using FlameSharp.Stacks;
using FlameSharp.Tokens;
using FlameSharp.Parsers;
using LLVMSharp;

namespace FlameSharp.Handlers
{
    public class Func
    {
        public static void Handle(List<Token> tokens, ref int i)
        {
            Token id = tokens[i + 1].Type == Token.TokenType.Identifier ? tokens[i + 1] : throw new Exception("error");
            Token param = tokens[i + 2].Type == Token.TokenType.Type ? tokens[i + 2] : throw new Exception("error");
            Token paramId = tokens[i + 3].Type == Token.TokenType.Identifier ? tokens[i + 3] : throw new Exception("error");
            Token symbol = tokens[i + 4].Type == Token.TokenType.Symbol ? tokens[i + 4] : throw new Exception("error");
            i += 4;

            List<Token> block = BlockParser.Generate(tokens, ref i);

            FuncStack.Push(LLVM.AddFunction(Parser.Module, id.Value, LLVMTypeRef.FunctionType(LLVMTypeRef.VoidType(), new LLVMTypeRef[] { LLVM.Int32Type() }, false)), id.Value);
            
            LLVMBasicBlockRef entry = FuncStack.Get(id.Value).AppendBasicBlock("entry");
            BlockParser.Parse(entry, block);
        }
    }
}
