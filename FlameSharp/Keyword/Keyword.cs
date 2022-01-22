using System;
using System.Linq;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public class Keyword : Token
    {
        public static string Pattern = @"let|if";

        public Keyword(int position, string value) : base(position, value) { }

        public void Handle(List<Token> tokens, ref int i)
        {
            int _i = i;
            switch (Value)
            {
                case "let":
                    {
                        if (tokens[i + 2] is not Identifier id) throw new Exception("error");
                        if (tokens[i + 3] is not Operator op || !(op.Value == "=")) throw new Exception("error");
                        if (tokens[i + 4] is not Type type) throw new Exception("error");
                        if (tokens.Where((x, j) => x.Value == ";" && j > _i).First() is not Symbol end) throw new Exception("error");

                        (LLVMValueRef value, LLVMTypeKind type) var = ExpressionParser.Handle(new List<Token>(tokens.ToArray()[(i + 6)..tokens.IndexOf(end)]));
                        LLVMValueRef ptr = LLVM.BuildAlloca(Parser.Builder, LLVM.Int32Type(), id.Value);
                        LLVM.BuildStore(Parser.Builder, var.value, ptr);

                        Stack.Add(id.Value, var);

                        i = tokens.IndexOf(end);
                        break;
                    }
                case "if":
                    {
                        // if: x == 10 -> { }
                        if (tokens.Where((x, j) => x.Value == "->" && j > _i).First() is not Symbol conditionEnd) throw new Exception("error");
                        if (tokens.Where((x, j) => x.Value == "{" && j > _i).First() is not Symbol blockStart) throw new Exception("error");
                        if (tokens.Where((x, j) => x.Value == "}" && j > _i).Last() is not Symbol blockEnd) throw new Exception("error");

                        (LLVMValueRef value, LLVMTypeKind type) var = ExpressionParser.Handle(new List<Token>(tokens.ToArray()[(i + 2)..tokens.IndexOf(conditionEnd)]));
                        LLVMBasicBlockRef ifBlock = LLVM.AppendBasicBlock(Parser.Scope, "if");
                        LLVMBasicBlockRef elseBlock = LLVM.AppendBasicBlock(Parser.Scope, "else");
                        LLVMBasicBlockRef mergeBlock = LLVM.AppendBasicBlock(Parser.Scope, "merge");

                        LLVM.BuildCondBr(Parser.Builder, var.value, ifBlock, elseBlock);

                        Parser.HandleBr(ifBlock, new List<Token>(tokens.ToArray()[(tokens.IndexOf(blockStart) + 1)..tokens.IndexOf(blockEnd)]), mergeBlock);
                        Parser.HandleBr(elseBlock, new List<Token>(), mergeBlock);

                        i = tokens.IndexOf(blockEnd);
                        // phi incoming if 

                        break;
                    }
                default:
                    throw new Exception("error");
            }
        }
    }
}
