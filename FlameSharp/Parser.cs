using System.Collections.Generic;
using FlameSharp.Components;
using LLVMSharp;

namespace FlameSharp
{
    public class Parser
    {
        public static LLVMModuleRef Module = LLVM.ModuleCreateWithName("");
        public static LLVMBuilderRef Builder = LLVM.CreateBuilder();

        public static void Handle(List<Token> tokens)
        {
            LLVMValueRef main = LLVM.AddFunction(Module, "main", LLVM.FunctionType(LLVMTypeRef.VoidType(), new LLVMTypeRef[] { }, false));
            LLVM.PositionBuilderAtEnd(Builder, main.AppendBasicBlock("entry"));

            for (int i = 0; i < tokens.Count; i++)
            {
                switch (true)
                {
                    case true when tokens[i] is Keyword:
                        (tokens[i] as Keyword).Handle(tokens, ref i);
                        continue;
                    case true when tokens[i] is Type:
                        (tokens[i] as Type).Handle(tokens, ref i);
                        continue;
                    case true when tokens[i] is Identifier:
                        (tokens[i] as Identifier).Handle(tokens, ref i);
                        continue;
                }
            }
        }
    }
}
