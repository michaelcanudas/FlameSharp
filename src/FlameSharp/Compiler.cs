using System.Collections.Generic;
using FlameSharp.Lexers;
using FlameSharp.Stacks;
using FlameSharp.Tokens;
using FlameSharp.Parsers;
using LLVMSharp;

namespace FlameSharp
{
    public class Compiler
    {
        public static string Compile(Config config)
        {
            List<Token> tokens = Lexer.Handle(config.Directory);

            FuncStack.Push(LLVM.AddFunction(Parser.Module, "main", LLVM.FunctionType(LLVMTypeRef.VoidType(), new LLVMTypeRef[] { }, false)), "main");
            BlockParser.Parse(FuncStack.Get("main").AppendBasicBlock("entry"), tokens);

            return Parser.Module.ToString();
        }
    }
}