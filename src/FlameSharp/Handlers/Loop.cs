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
    public class Loop
    {
        public static void Handle(List<Token> tokens, ref int i)
        {
            // loops??
            // create loop block
            // br at end of block
            // ? goto loop block
            // : goto exit block
        }
    }
}
