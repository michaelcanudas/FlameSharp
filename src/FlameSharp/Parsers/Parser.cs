using System.Collections.Generic;
using FlameSharp.Tokens;
using LLVMSharp;

namespace FlameSharp.Parsers
{
    public class Parser
    {
        public static LLVMModuleRef Module = LLVM.ModuleCreateWithName("");
        public static LLVMBuilderRef Builder = LLVM.CreateBuilder();

        public static void Parse(LLVMBasicBlockRef scope, List<Token> tokens)
        {
        }
    }
}
