using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts.IO
{
    public class IoAdapter<TBusDataType> : ISettable, IEnablable, IInputable<TBusDataType> where TBusDataType : IList<bool>
    {
        private readonly TBusDataType _address;
        private readonly IMemoryGate _memoryGate;
        private readonly IXOr _xOr;

        public bool Set { get; set; }
        public bool Enable { get; set; }

        public bool DataAddress { get; set; }

        public bool InputOutput { get; set; }
        
        public TBusDataType Input { get; set; }

        public IoAdapter(TBusDataType address, 
            IMemoryGate memoryGate,
            IXOr xOr)
        {
            _address = address;
            _memoryGate = memoryGate;
            _xOr = xOr;
        }

        protected bool MemoryGateOutput()
        {
            return _memoryGate.Apply(IsAdapterAddress(), ClkSOfOutInstruction());
        }

        private bool IsAdapterAddress()
        {
            for (var i = 0; i < Input.Count; i++)
            {
                var unequal = _xOr.ApplyParams(Input[i], _address[i]);

                if (unequal)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ClkSOfOutInstruction()
        {
            return Set && InputOutput && DataAddress;
        }
    }
}