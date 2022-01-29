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

            LLVMBasicBlockRef condBlock = FuncStack.Get("main").AppendBasicBlock("cond");
            LLVMBasicBlockRef loopBlock = FuncStack.Get("main").AppendBasicBlock("loop");
            LLVMBasicBlockRef mergeBlock = FuncStack.Get("main").AppendBasicBlock("merge");

            // use phi for conditions (in if's too?)

            /*LLVM.Set
            LLVMValueRef vv = LLVM.BuildPhi(Parser.Builder, LLVMTypeRef.Int32Type(), "phi");
            vv.AddIncoming(
                new LLVMValueRef[] { var.value,  },
                new LLVMBasicBlockRef[] { FuncStack.Get("main").GetEntryBasicBlock(), loopBlock },
                2);*/

            LLVM.PositionBuilderAtEnd(Parser.Builder, condBlock);
            LLVM.BuildCondBr(Parser.Builder, var.value, loopBlock, mergeBlock);

            BlockParser.Parse(loopBlock, mergeBlock, block);

            LLVM.PositionBuilderAtEnd(Parser.Builder, loopBlock);
            LLVM.BuildBr(Parser.Builder, condBlock);
            LLVM.PositionBuilderAtEnd(Parser.Builder, BlockParser.Scope);
        }
    }
}
