using System;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Operator
    {
        private (LLVMValueRef, LLVMTypeKind) ParseEq(List<Token> lhs, List<Token> rhs, HandleType type)
        {
            (LLVMValueRef value, LLVMTypeKind type) lVar = ExpressionParser.Handle(lhs);
            (LLVMValueRef value, LLVMTypeKind type) rVar = ExpressionParser.Handle(rhs);
            if (lVar.type != rVar.type) throw new Exception("error");

            return type switch
            {
                HandleType.Signed => (LLVM.BuildICmp(Parser.Builder, LLVMIntPredicate.LLVMIntEQ, lVar.value, rVar.value, ""), lVar.type),
                HandleType.Unsigned => (LLVM.BuildICmp(Parser.Builder, LLVMIntPredicate.LLVMIntEQ, lVar.value, rVar.value, ""), lVar.type),
                HandleType.Float => (LLVM.BuildFCmp(Parser.Builder, LLVMRealPredicate.LLVMRealOEQ, lVar.value, rVar.value, ""), lVar.type),
            };
        }
    }
}
