using System;
using System.Linq;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Keyword
    {
        private void HandleVar(List<Token> tokens, ref int i)
        {
            Type type = tokens[i + 2] is Type ? tokens[i + 2] as Type : throw new Exception("error");
            Identifier id = tokens[i + 3] is Identifier ? tokens[i + 3] as Identifier : throw new Exception("error");
            Symbol equals = tokens[i + 4] is Symbol && tokens[i + 4].Value == "=" ? tokens[i + 4] as Symbol : throw new Exception("error");
            Symbol end = tokens.Where((x, j) => x.Value == ";" && j > i).First() is Symbol ? tokens.Where((x, j) => x.Value == ";" && j > i).First() as Symbol : throw new Exception("error");

            LLVMValueRef value = ExpressionParser.Handle(new List<Token>(tokens.ToArray()[(i + 5)..(tokens.IndexOf(end))]));
            LLVMValueRef ptr = LLVM.BuildAlloca(Parser.Builder, type.Convert(), id.Value);
            LLVM.BuildStore(Parser.Builder, value, ptr);
        }
    }
}
