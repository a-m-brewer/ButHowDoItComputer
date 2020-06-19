using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public BusMessage<T> Data { get; set; }

        public virtual void UpdateData(BusMessage<T> input)
        {
            Data = input;
        }

        public virtual void UpdateSubs()
        {
            Parallel.ForEach(_registers, register =>
            {
                register.Input = Data.Data;

                if (!register.Set && !register.Enable)
                {
                    return;
                }

                if (register.Name != Data.Name)
                {
                    register.Apply();
                }
            });

            Parallel.ForEach(_bytes, action => action(Data.Data));
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