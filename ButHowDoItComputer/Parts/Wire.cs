using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Wire<T> : IWire<T>
    {
        private T _output;

        public Wire(T output)
        {
            _output = output;
        }

        public void Update(T input)
        {
            _output = input;
        }
    }
}