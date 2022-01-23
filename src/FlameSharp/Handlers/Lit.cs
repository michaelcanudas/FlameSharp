using FlameSharp.Stacks;
using LLVMSharp;

namespace FlameSharp.Handlers
{
    public class Lit
    {
        public static void Handle(string val)
        {
            ValueStack.Push((LLVM.ConstInt(LLVM.Int32Type(), ulong.Parse(val), true), LLVMTypeKind.LLVMIntegerTypeKind));
        }
    }
}
