﻿using System;
using System.Linq;
using System.Collections.Generic;
using FlameSharp.Stacks;
using FlameSharp.Tokens;
using FlameSharp.Parsers;
using LLVMSharp;
using FlameSharp.Utils;

namespace FlameSharp.Handlers
{
    public class If
    {
        public static void Handle(List<Token> tokens, ref int i)
        {
            int _i = i;

            Token conditionStart = tokens.Where((x, j) => x.Value == ":" && x.Type == Token.TokenType.Symbol && j > _i).First();
            Token conditionEnd = tokens.Where((x, j) => x.Value == "->" && x.Type == Token.TokenType.Symbol && j > _i).First();
            Token blockStart = tokens.Where((x, j) => x.Value == "{" && x.Type == Token.TokenType.Symbol && j > _i).First();
            Token blockEnd = Brackets.FindClosingBracket(tokens, tokens.IndexOf(blockStart));
            if (conditionStart == null || conditionEnd == null || blockStart == null || blockEnd == null) throw new Exception("error");

            ValueParser.Parse(new List<Token>(tokens.ToArray()[(tokens.IndexOf(conditionStart) + 1)..tokens.IndexOf(conditionEnd)]));
            (LLVMValueRef value, LLVMTypeKind type) var = ValueStack.Pop();

            LLVMBasicBlockRef ifBlock = FuncStack.Get("main").AppendBasicBlock("if");
            LLVMBasicBlockRef elseBlock = FuncStack.Get("main").AppendBasicBlock("else");
            LLVMBasicBlockRef mergeBlock = FuncStack.Get("main").AppendBasicBlock("merge");

            LLVM.BuildCondBr(Parser.Builder, var.value, ifBlock, elseBlock);
            BlockParser.Parse(ifBlock, mergeBlock, new List<Token>(tokens.ToArray()[(tokens.IndexOf(blockStart) + 1)..tokens.IndexOf(blockEnd)]));
            BlockParser.Parse(elseBlock, mergeBlock, new List<Token>());

            i = tokens.IndexOf(blockEnd);
        }
    }
}