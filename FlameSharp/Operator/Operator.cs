using System;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Operator : Token
    {
        public static string[] Patten = { };

        public Operator(string value) : base(value) { }

        public LLVMValueRef Handle(List<Token> lhs, List<Token> rhs, HandleType type)
        {
            switch (Value)
            {
                case "+":
                    return HandleAdd(lhs, rhs, type);
                case "-":
                    return HandleSub(lhs, rhs, type);
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
