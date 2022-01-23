using System.Collections.Generic;
using FlameSharp.Parsers;
using FlameSharp.Stacks;
using FlameSharp.Tokens;
using LLVMSharp;

namespace FlameSharp.Handlers
{
    public class Div
    {
        public static void Handle(List<Token> lTokens, List<Token> rTokens)
        {
            ValueParser.Parse(lTokens);
            (LLVMValueRef, LLVMTypeKind) l = ValueStack.Pop();
            ValueParser.Parse(rTokens);
            (LLVMValueRef, LLVMTypeKind) r = ValueStack.Pop();

            ValueStack.Push((LLVM.BuildSDiv(Parser.Builder, l.Item1, r.Item1, ""), LLVMTypeKind.LLVMIntegerTypeKind));
        }
    }
}
