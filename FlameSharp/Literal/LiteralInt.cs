using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Literal
    {
        private (LLVMValueRef, LLVMTypeKind) ParseInt()
        {
            return (LLVM.ConstInt(LLVM.Int32Type(), ulong.Parse(Value), true), LLVMTypeKind.LLVMIntegerTypeKind);
        }
    }
}
