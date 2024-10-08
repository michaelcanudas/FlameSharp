﻿using System;
using System.Linq;
using System.Collections.Generic;
using FlameSharp.Tokens;
using FlameSharp.Handlers;
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
                // future: neated this loop to prevent a bunch of "if" from building up with new types

                if (tokens[i].Type == Token.TokenType.Keyword)
                {
                    switch (tokens[i].Value)
                    {
                        case "let":
                            Let.Handle(tokens, ref i);
                            break;
                        case "if":
                            If.Handle(tokens, ref i);
                            break;
                        /*case "loop":
                            Loop.Handle(tokens, ref i);
                            break;*/
                    }
                }
                else if (tokens[i].Type == Token.TokenType.Identifier)
                {
                    //Assign.Handle(tokens, ref i);
                    throw new Exception("error");
                }
                else throw new Exception("error");
            }

            Scope = preScope;
            LLVM.PositionBuilderAtEnd(Parser.Builder, Scope);
        }
        public static void Parse(LLVMBasicBlockRef scope, LLVMBasicBlockRef nextScope, List<Token> tokens)
        {
            Parse(scope, tokens);

            Scope = nextScope;
            LLVM.PositionBuilderAtEnd(Parser.Builder, Scope);
        }

        public static List<Token> Generate(List<Token> tokens, ref int i)
        {
            int _i = i;

            Token start = tokens.Where((x, j) =>
                x.Type ==  Token.TokenType.Symbol &&
                x.Value == "{" &&
                j > _i
            ).First();
            int startIndex = tokens.IndexOf(start) + 1;

            int stack = 0;
            Token end = tokens.Where((x, j) => {
                if (j < startIndex) return false;
                if (x.Type !=  Token.TokenType.Symbol) return false;

                if (x.Value == "}" && stack == 0)
                {
                    return true;
                }
                else if (x.Value == "}")
                {
                    stack--;
                }
                else if (x.Value == "{")
                {
                    stack++;
                }
                
                return false;
            }).First();
            int endIndex = tokens.IndexOf(end);
            
            // find way to throw error

            i = endIndex;

            return new List<Token>(tokens.ToArray()[startIndex..endIndex]);
        }
    }
}