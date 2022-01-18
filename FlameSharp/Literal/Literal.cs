using System;
using System.Text.RegularExpressions;
using LLVMSharp;

namespace FlameSharp.Components
{
    public partial class Literal : Token
    {
        public static string Pattern = @"\b[0-9]+\b";

        public Literal(int position, string value) : base(position, value) { }

        public LLVMValueRef Parse()
        {
            switch (true)
            {
                case true when Regex.IsMatch(Value, @"\b[0-9]+\b"):
                    return ParseInt();
                default:
                    throw new Exception("error");
            }
        }
    }
}
