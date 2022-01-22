﻿using System;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Operator : Token
    {
        public static string Pattern = @"==|=|\+|-|\*|\/";

        public Operator(int position, string value) : base(position, value) { }

        public (LLVMValueRef, LLVMTypeKind) ParseBinary(List<Token> lhs, List<Token> rhs, HandleType type)
        {
            switch (Value)
            {
                case "==":
                    return ParseEq(lhs, rhs, type);
                case "+":
                    return ParseAdd(lhs, rhs, type);
                case "-":
                    return ParseSub(lhs, rhs, type);
                case "*":
                    return ParseMul(lhs, rhs, type);
                case "/":
                    return ParseDiv(lhs, rhs, type);
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
