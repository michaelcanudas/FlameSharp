using System;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Operator
    {
        private (LLVMValueRef, LLVMTypeKind) ParseSub(List<Token> lhs, List<Token> rhs, HandleType type)
        {
            (LLVMValueRef value, LLVMTypeKind type) lVal = ExpressionParser.Handle(lhs);
            (LLVMValueRef value, LLVMTypeKind type) rVal = ExpressionParser.Handle(rhs);
            if (lVal.type != rVal.type) throw new Exception("error");

            return type switch
            {
                HandleType.Float => (LLVM.BuildFSub(Parser.Builder, lVal.value, rVal.value, ""), lVal.type),
                _ => (LLVM.BuildSub(Parser.Builder, lVal.value, rVal.value, ""), lVal.type)
            };
        }
    }
}
