using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Stacks
{
    public class FuncStack
    {
        static Dictionary<string, LLVMValueRef> identifiers = new Dictionary<string, LLVMValueRef>();

        public static void Push(LLVMValueRef value, string key)
        {
            identifiers.Add(key, value);
        }

        public static LLVMValueRef Get(string key)
        {
            return identifiers[key];
        }
    }
}
