using System;
using System.Collections.Generic;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Type : Token
    {
        public static string Pattern = "i32";

        public Type(int position, string value) : base(position, value) { }

        public void Handle(List<Token> tokens, ref int i)
        {
            switch (Value)
            {
                case "i32":
                    HandleI32(tokens, ref i);
                    break;
                default:
                    throw new Exception("error");
            }
        }


        // REPLACE WITH TYPE CONVERTER UTIL
        public LLVMTypeRef Convert()
        {
            return Value switch
            {
                "i32" => LLVMTypeRef.Int32Type()
            };
        }
    }
}
