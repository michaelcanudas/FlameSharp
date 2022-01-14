using LLVMSharp;

namespace FlameSharp.Components
{
    public class Type : Token
    {
        public static string[] Patten = { "i32" };

        public Type(string value) : base(value) { }

        public LLVMTypeRef Handle()
        {
            return Value switch
            {
                "i32" => LLVMTypeRef.Int32Type()
            };
        }
    }
}
