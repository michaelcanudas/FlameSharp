using System;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Operator : Token
    {
        public static string Pattern = @"=|\+|-";

        public Operator(int position, string value) : base(position, value) { }

        public LLVMValueRef ParseBinary(List<Token> lhs, List<Token> rhs, HandleType type)
        {
            switch (Value)
            {
                case "+":
                    return ParseAdd(lhs, rhs, type);
                case "-":
                    return ParseSub(lhs, rhs, type);
                default:
                    throw new Exception("error");
            }
        }

        public enum HandleType
        {
            Signed,
            Unsigned,
            Float
        }
    }
}
