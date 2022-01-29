using System.Collections.Generic;
using FlameSharp.Stacks;
using FlameSharp.Tokens;
using FlameSharp.Parsers;
using LLVMSharp;

namespace FlameSharp.Handlers
{
    public class If
    {
        public static void Handle(List<Token> tokens, ref int i)
        {
            List<Token> value = ValueParser.Generate(tokens, ref i);
            List<Token> block = BlockParser.Generate(tokens, ref i);

            ValueParser.Parse(value);
            (LLVMValueRef value, LLVMTypeKind type) var = ValueStack.Pop();

            LLVMBasicBlockRef ifBlock = FuncStack.Get("main").AppendBasicBlock("if");
            LLVMBasicBlockRef elseBlock = FuncStack.Get("main").AppendBasicBlock("else");
            LLVMBasicBlockRef mergeBlock = FuncStack.Get("main").AppendBasicBlock("merge");

            LLVM.BuildCondBr(Parser.Builder, var.value, ifBlock, elseBlock);
            BlockParser.Parse(ifBlock, mergeBlock, block);
            BlockParser.Parse(elseBlock, mergeBlock, new List<Token>());
        }
    }
}
