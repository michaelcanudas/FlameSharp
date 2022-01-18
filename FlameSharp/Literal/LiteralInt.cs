using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Literal
    {
        private LLVMValueRef ParseInt()
        {
            return LLVM.ConstInt(LLVM.Int32Type(), ulong.Parse(Value), true);
        }
    }
}
