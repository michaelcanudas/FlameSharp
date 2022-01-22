using System;
using System.Linq;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public class Keyword : Token
    {
        public static string Pattern = @"let";

        public Keyword(int position, string value) : base(position, value) { }

        public void Handle(List<Token> tokens, ref int i)
        {
            switch (Value)
            {
                case "let":
                    int _i = i;

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
                default:
                    throw new Exception("error");
            }
        }
    }
}
