using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class BitWire<T> : IWire<T>
    {
        private readonly IInputable<T> _output;

        public BitWire(IInputable<T> output)
        {
            _output = output;
        }

        public void Update(T input)
        {
            _output.UpdateInput(input);
        }
    }
}