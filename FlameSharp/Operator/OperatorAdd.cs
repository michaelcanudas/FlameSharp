using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Operator
    {
        private LLVMValueRef ParseAdd(List<Token> lhs, List<Token> rhs, HandleType type)
        {
            LLVMValueRef lVal = ExpressionParser.Handle(lhs);
            LLVMValueRef rVal = ExpressionParser.Handle(rhs);

            return type switch
            {
                HandleType.Signed => LLVM.BuildAdd(Parser.Builder, lVal, rVal, ""),
                HandleType.Unsigned => LLVM.BuildAdd(Parser.Builder, lVal, rVal, ""),
                HandleType.Float => LLVM.BuildAdd(Parser.Builder, lVal, rVal, "")
            };
        }
    }
}
