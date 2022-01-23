using FlameSharp.Stacks;

namespace FlameSharp.Handlers
{
    public class Id
    {
        public static void Handle(string key)
        {
            ValueStack.Push(ValueStack.Get(key));
        }
    }
}
