using System.Collections.Generic;
using FlameSharp.Parsers;
using FlameSharp.Lexers;
using FlameSharp.Tokens;
using LLVMSharp;
using System.IO;

namespace FlameSharp
{
    public class Compiler
    {
        public static void Compile(Config config)
        {
            List<Token> tokens = Lexer.Handle(File.ReadAllText(config.Directory));

            Parser.Parse(tokens);

            LLVM.DumpModule(Parser.Module);
        }

        /*
        move utils out of utils classes to simplify
        go over parsers
        */
    }
}