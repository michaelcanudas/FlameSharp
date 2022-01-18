using System;
using System.Linq;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Type
    {
        private void HandleI32(List<Token> tokens, ref int i)
        {
            int _i = i;

            Identifier id = tokens[i + 2] is Identifier
                ? tokens[i + 2] as Identifier
                : throw new Exception("error");
            Operator equals = tokens[i + 3] is Operator && tokens[i + 3].Value == "="
                ? tokens[i + 3] as Operator
                : throw new Exception("error");
            Symbol end = tokens.Where((x, j) => x.Value == ";" && j > _i).First() is Symbol
                ? tokens.Where((x, j) => x.Value == ";" && j > _i).First() as Symbol
                : throw new Exception("error");

            LLVMValueRef value = ExpressionParser.Handle(new List<Token>(tokens.ToArray()[(i + 4)..tokens.IndexOf(end)]));
            LLVMValueRef ptr = LLVM.BuildAlloca(Parser.Builder, LLVM.Int32Type(), id.Value);
            LLVM.BuildStore(Parser.Builder, value, ptr);

            i = tokens.IndexOf(end);
        }
    }
}
