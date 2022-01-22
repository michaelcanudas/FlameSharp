using System;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Operator
    {
        private (LLVMValueRef, LLVMTypeKind) ParseDiv(List<Token> lhs, List<Token> rhs, HandleType type)
        {
            (LLVMValueRef value, LLVMTypeKind type) lVal = ExpressionParser.Handle(lhs);
            (LLVMValueRef value, LLVMTypeKind type) rVal = ExpressionParser.Handle(rhs);
            if (lVal.type != rVal.type) throw new Exception("error");

            return type switch
            {
                HandleType.Signed => (LLVM.BuildSDiv(Parser.Builder, lVal.value, rVal.value, ""), lVal.type),
                HandleType.Unsigned => (LLVM.BuildUDiv(Parser.Builder, lVal.value, rVal.value, ""), lVal.type),
                HandleType.Float => (LLVM.BuildFDiv(Parser.Builder, lVal.value, rVal.value, ""), lVal.type)
            };
        }
    }
}
