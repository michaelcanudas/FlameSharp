using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Stacks
{
    public class ValueStack
    {
        static Stack<(LLVMValueRef, LLVMTypeKind)> literals = new Stack<(LLVMValueRef, LLVMTypeKind)>();
        static Dictionary<string, (LLVMValueRef, LLVMTypeKind)> identifiers = new Dictionary<string, (LLVMValueRef, LLVMTypeKind)>();

        public static void Push((LLVMValueRef, LLVMTypeKind) value)
        {
            literals.Push(value);
        }

        public static (LLVMValueRef, LLVMTypeKind) Pop()
        {
            return literals.Pop();
        }

        public static void Push((LLVMValueRef, LLVMTypeKind) value, string key)
        {
            identifiers.Add(key, value);
        }

        public static void Put(string key, (LLVMValueRef, LLVMTypeKind) value)
        {
            identifiers[key] = value;
        }

        public static (LLVMValueRef, LLVMTypeKind) Get(string key)
        {
            return identifiers[key];
        }
    }
}
