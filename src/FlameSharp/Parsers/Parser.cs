using System;
using System.Collections.Generic;
using FlameSharp.Tokens;
using FlameSharp.Handlers;
using LLVMSharp;

namespace FlameSharp.Parsers
{
    public class Parser
    {
        public static LLVMModuleRef Module = LLVM.ModuleCreateWithName("");
        public static LLVMBuilderRef Builder = LLVM.CreateBuilder();

        public static void Parse(List<Token> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type != Token.TokenType.Keyword) throw new Exception("error");

                switch (tokens[i].Value)
                {
                    case "func":
                        Func.Handle(tokens, ref i);
                        break;
                }
            }
        }
    }
}
