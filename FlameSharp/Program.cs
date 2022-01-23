using System.IO;
using System.Collections.Generic;
using FlameSharp.Lexers;
using FlameSharp.Stacks;
using FlameSharp.Tokens;
using FlameSharp.Parsers;
using LLVMSharp;

namespace FlameSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Token> tokens = Lexer.Handle(File.ReadAllText("./Examples/test.flm"));

            FuncStack.Push(LLVM.AddFunction(Parser.Module, "main", LLVM.FunctionType(LLVMTypeRef.VoidType(), new LLVMTypeRef[] { }, false)), "main");
            BlockParser.Parse(FuncStack.Get("main").AppendBasicBlock("entry"), tokens);

            LLVM.DumpModule(Parser.Module);
        }
    }
}
