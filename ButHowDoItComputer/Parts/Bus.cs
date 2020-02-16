using System;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Bus<T> : IBus<T>
    {
        private readonly List<IRegister<T>> _registers;
        private readonly List<Action<T>> _bytes;

        public Bus(BusMessage<T> startState)
        {
            Data = startState;
            _registers = new List<IRegister<T>>();
            _bytes = new List<Action<T>>();
        }

        public Bus()
        {
            
        }
        
        public BusMessage<T> Data { get; set; }

        public void UpdateData(BusMessage<T> input)
        {
            Data = input;
        }

        public void UpdateSubs()
        {
            foreach (var register in _registers)
            {
                register.Input = Data.Data;

                if (register.Name != Data.Name)
                {
                    register.Apply();
                }
            }

            foreach (var update in _bytes)
            {
                update(Data.Data);
            }
        }

        public void AddRegister(IRegister<T> updateFunc)
        {
            _registers.Add(updateFunc);
        }

        public void AddByte(Action<T> updateByte)
        {
            _bytes.Add(updateByte);
        }
    }

    public interface IBus<T> : IData<BusMessage<T>> 
    {
        void UpdateData(BusMessage<T> input);
        void UpdateSubs();
        void AddRegister(IRegister<T> updateFunc);
        void AddByte(Action<T> updateByte);
    }
}