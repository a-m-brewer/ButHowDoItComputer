using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.IO
{
    public class KeyboardAdapter<TBusDataType> : IoAdapter<TBusDataType>, IApplicable where TBusDataType : IBusDataType

    {
        private readonly IKeyboardBuffer<TBusDataType> _keyboardBuffer;
        private readonly IRegister<TBusDataType> _keyboardRegister;
        private readonly INot _not;
        private readonly IAnd _and;

        public TBusDataType Ouput { get; set; }

        public KeyboardAdapter(
            IKeyboardBuffer<TBusDataType> keyboardBuffer,
            IRegister<TBusDataType> keyboardRegister,
            IBusDataTypeFactory<TBusDataType> busDataTypeFactory, INot not,
            IMemoryGate memoryGate, IAnd and, IXOr xOr) : base(busDataTypeFactory.CreateParams(
            new []{true, true, true, true, false, false, false, false}), memoryGate, and, xOr)
        {
            _keyboardBuffer = keyboardBuffer;
            _keyboardRegister = keyboardRegister;
            _keyboardRegister.Set = true;
            _not = not;
            _and = and;
        }

        public void Apply()
        {
            var memoryBitResult = MemoryGateOutput();
            var memoryBitAndClkEOfInput = _and.ApplyParams(memoryBitResult, ClkEOfInInstruction());
            
            _keyboardRegister.Enable = memoryBitAndClkEOfInput;

            if (_keyboardRegister.Enable)
            {
                var input = _keyboardBuffer.Pop();
                _keyboardRegister.Input = input;
                _keyboardRegister.Apply();
            }
            
            Ouput = _keyboardRegister.Output;
        }

        private bool ClkEOfInInstruction()
        {
            return _and.ApplyParams(Enable, _not.Apply(DataAddress), _not.Apply(InputOutput));
        }
    }
}