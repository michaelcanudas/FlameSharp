using System;
using System.Linq;
using System.Collections.Generic;
using FlameSharp.Utils;
using FlameSharp.Stacks;
using FlameSharp.Tokens;
using FlameSharp.Parsers;
using LLVMSharp;

namespace FlameSharp.Handlers
{
    public class If
    {
        public static void Handle(List<Token> tokens, ref int i)
        {
            int _i = i;

            List<Token> value = ValueParser.Generate(tokens, ref i);
            List<Token> block = BlockParser.Generate(tokens, ref i);
            /*
            Token conditionStart = tokens[_i + 1];
            Token conditionEnd = tokens.Where((x, j) => x.Value == "->" && x.Type == Token.TokenType.Symbol && j > _i).First();
            
            Token blockStart = tokens.IndexOf(conditionEnd) + 1;
            Token blockEnd = Brackets.FindClosingBracket(tokens, tokens.IndexOf(blockStart - 1));
            
            if (conditionStart == null || conditionEnd == null || blockStart == null || blockEnd == null) throw new Exception("error");
            */

            //ValueParser.Parse(new List<Token>(tokens.ToArray()[tokens.IndexOf(conditionStart)..tokens.IndexOf(conditionEnd)]));
            ValueParser.Parse(value);
            (LLVMValueRef value, LLVMTypeKind type) var = ValueStack.Pop();

            LLVMBasicBlockRef ifBlock = FuncStack.Get("main").AppendBasicBlock("if");
            LLVMBasicBlockRef elseBlock = FuncStack.Get("main").AppendBasicBlock("else");
            LLVMBasicBlockRef mergeBlock = FuncStack.Get("main").AppendBasicBlock("merge");

            LLVM.BuildCondBr(Parser.Builder, var.value, ifBlock, elseBlock);
            //BlockParser.Parse(ifBlock, mergeBlock, new List<Token>(tokens.ToArray()[tokens.IndexOf(blockStart)..tokens.IndexOf(blockEnd)]));
            BlockParser.Parse(ifBlock, mergeBlock, block);
            BlockParser.Parse(elseBlock, mergeBlock, new List<Token>());
        }
    }
}
