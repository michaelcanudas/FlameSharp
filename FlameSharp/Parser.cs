using System.Collections.Generic;
using FlameSharp.Components;
using LLVMSharp;

namespace FlameSharp
{
    public class Parser
    {
        public static LLVMModuleRef Module = LLVM.ModuleCreateWithName("");
        public static LLVMBuilderRef Builder = LLVM.CreateBuilder();
        public static LLVMValueRef Scope;

        public static void Handle(LLVMBasicBlockRef scope, List<Token> tokens)
        {
            LLVM.PositionBuilderAtEnd(Builder, scope);

            for (int i = 0; i < tokens.Count; i++)
            {
                switch (true)
                {
                    case true when tokens[i] is Keyword:
                        (tokens[i] as Keyword).Handle(tokens, ref i);
                        continue;
                    case true when tokens[i] is Type:
                        (tokens[i] as Type).Handle(tokens, ref i); // check this
                        continue;
                    case true when tokens[i] is Identifier:
                        (tokens[i] as Identifier).Handle(tokens, ref i);
                        continue;
                }
            }
        }

        public static void HandleBr(LLVMBasicBlockRef scope, List<Token> tokens, LLVMBasicBlockRef br)
        {
            Handle(scope, tokens);
            LLVM.BuildBr(Builder, br);
        }
    }
}
