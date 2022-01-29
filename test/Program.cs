using FlameSharp;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Config c = new Config("./test.flm");
            Compiler.Compile(c);
        }
    }
}
