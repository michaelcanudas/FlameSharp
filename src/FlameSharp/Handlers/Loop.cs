using System.Collections.Generic;
using FlameSharp.Stacks;
using FlameSharp.Tokens;
using FlameSharp.Parsers;
using LLVMSharp;

namespace FlameSharp.Handlers
{
    public class Loop
    {
        public static void Handle(List<Token> tokens, ref int i)
        {
            List<Token> value = ValueParser.Generate(tokens, ref i);
            List<Token> block = BlockParser.Generate(tokens, ref i);

            ValueParser.Parse(value);
            (LLVMValueRef value, LLVMTypeKind type) var = ValueStack.Pop();

            LLVMBasicBlockRef loopBlock = FuncStack.Get("main").AppendBasicBlock("loop");
            LLVMBasicBlockRef mergeBlock = FuncStack.Get("main").AppendBasicBlock("merge");

            LLVM.BuildCondBr(Parser.Builder, var.value, loopBlock, mergeBlock);
            BlockParser.Parse(loopBlock, mergeBlock, block);

            // somehow insert br condition inside loop block

            // loops??
            // create loop block
            // br at end of block
            // ? goto loop block
            // : goto exit block
        }
    }
}
