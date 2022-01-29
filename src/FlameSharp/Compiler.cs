using System.IO;
using System.Collections.Generic;
using FlameSharp.Lexers;
using FlameSharp.Tokens;
using FlameSharp.Parsers;
using LLVMSharp;


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
    }
}