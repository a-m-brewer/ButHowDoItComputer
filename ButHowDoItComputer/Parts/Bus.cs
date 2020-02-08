using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Components.CpuSubscribers;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Parts
{
    public class Bus : IBus
    {
        private readonly IList<IRegister<IByte>> _registers;

        public List<IBusInputSubscriber<IByte>> BusSubscribers { get; set; } = new List<IBusInputSubscriber<IByte>>();
        
        public IByte State { get; private set; }

        public Bus(IList<IRegister<IByte>> registers, IByteFactory byteFactory)
        {
            _registers = registers;
            State = byteFactory.Create();
            BusSubscribers.AddRange(_registers);
        }
        
        public IEnumerator<IRegister<IByte>> GetEnumerator()
        {
            return _registers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IRegister<IByte> item)
        {
            _registers.Add(item);
            BusSubscribers.Add(item);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(IRegister<IByte> item)
        {
            return _registers.Contains(item);
        }

        public void CopyTo(IRegister<IByte>[] array, int arrayIndex)
        {
            _registers.CopyTo(array, arrayIndex);
        }

        public bool Remove(IRegister<IByte> item)
        {
            BusSubscribers.Remove(item);
            return _registers.Remove(item);
        }

        public int Count => _registers.Count;

        public bool IsReadOnly => _registers.IsReadOnly;
        
        public int IndexOf(IRegister<IByte> item)
        {
            return _registers.IndexOf(item);
        }

        public void Insert(int index, IRegister<IByte> item)
        {
            _registers.Insert(index, item);
            BusSubscribers.Add(_registers[index]);
        }

        public void RemoveAt(int index)
        {
            _registers.RemoveAt(index);
        }

        public IRegister<IByte> this[int index]
        {
            get => _registers[index];
            set
            {
                _registers[index] = value;
                BusSubscribers.Add(value);
            }
        }

        public void Apply()
        {
            foreach (var register in _registers)
            {
                var result = register.Apply(State);
                if (!register.Enable) continue;
                
                State = result;

                foreach (var busSubscriber in BusSubscribers.Where(sub => register != sub))
                {
                    busSubscriber.Input = State;
                    busSubscriber.Apply();
                }
            }
        }

        public IByte Input
        {
            get => State; set => State = value; 
        }
    }
}