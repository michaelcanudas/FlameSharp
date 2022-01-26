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

        public static List<Token> Generate(List<Token> tokens, ref int i)
        {
            int _i = i;

           Token start = tokens.Where((x, j) => {
               x.Type == TokenType.Symbol &&
               x.Value == "{" &&
               j > i
           }).First();
           int startIndex = tokens.IndexOf(start) + 1;

           Token end =  tokens.Where((x, j) => {
               x.Type == TokenType.Symbol &&
               x.Value == "}" &&
               j > startIndex &&
               // call function to add to stack and check if stack == 0
           }).First();
           int endIndex = tokens.IndexOf(end);
           
           // find way to throw error

           i = endIndex + 1;

           return new List<Token>(tokens.ToArray()[startIndex..endIndex]);
        }
    }
}
/*
if {

}

if {
    if {

    }
}

if {

}
*/