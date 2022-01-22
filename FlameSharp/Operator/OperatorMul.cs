using System;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Operator
    {
        private (LLVMValueRef, LLVMTypeKind) ParseMul(List<Token> lhs, List<Token> rhs, HandleType type)
        {
            (LLVMValueRef value, LLVMTypeKind type) lVar = ExpressionParser.Handle(lhs);
            (LLVMValueRef value, LLVMTypeKind type) rVar = ExpressionParser.Handle(rhs);
            if (lVar.type != rVar.type) throw new Exception("error");

            return type switch
            {
                HandleType.Float => (LLVM.BuildFMul(Parser.Builder, lVar.value, rVar.value, ""), lVar.type),
                _ => (LLVM.BuildMul(Parser.Builder, lVar.value, rVar.value, ""), lVar.type)
            };
        }
    }
}
