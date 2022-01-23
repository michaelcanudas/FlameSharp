using System;
using System.Collections.Generic;
using FlameSharp.Handlers;
using FlameSharp.Tokens;
using LLVMSharp;

namespace FlameSharp.Parsers
{
    public class BlockParser
    {
        public static LLVMBasicBlockRef Scope;

        public static void Parse(LLVMBasicBlockRef scope, List<Token> tokens)
        {
            LLVMBasicBlockRef preScope = Scope;

            Scope = scope;
            LLVM.PositionBuilderAtEnd(Parser.Builder, Scope);
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type != Token.TokenType.Keyword) throw new Exception("error");

                switch (tokens[i].Value)
                {
                    case "let":
                        Let.Handle(tokens, ref i);
                        break;
                    case "if":
                        If.Handle(tokens, ref i);
                        break;
                }
            }
            Scope = preScope;
            LLVM.PositionBuilderAtEnd(Parser.Builder, Scope);
        }

        public static void Parse(LLVMBasicBlockRef scope, LLVMBasicBlockRef nextScope, List<Token> tokens)
        {
            Parse(scope, tokens);

            Scope = nextScope;
            LLVM.PositionBuilderAtEnd(Parser.Builder, nextScope);
        }
    }
}
