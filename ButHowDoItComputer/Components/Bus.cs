using System.Collections;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class Bus : IList<IRegister>
    {
        private readonly IList<IRegister> _registers;
        private IByte _bus;

        public Bus(IList<IRegister> registers, IByteFactory byteFactory)
        {
            _registers = registers;
            _bus = byteFactory.Create();
        }
        
        public IEnumerator<IRegister> GetEnumerator()
        {
            return _registers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IRegister item)
        {
            _registers.Add(item);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(IRegister item)
        {
            return _registers.Contains(item);
        }

        public void CopyTo(IRegister[] array, int arrayIndex)
        {
            _registers.CopyTo(array, arrayIndex);
        }

        public bool Remove(IRegister item)
        {
            return _registers.Remove(item);
        }

        public int Count => _registers.Count;

        public bool IsReadOnly => _registers.IsReadOnly;
        
        public int IndexOf(IRegister item)
        {
            return _registers.IndexOf(item);
        }

        public void Insert(int index, IRegister item)
        {
            _registers.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _registers.RemoveAt(index);
        }

        public IRegister this[int index]
        {
            get => _registers[index];
            set => _registers[index] = value;
        }

        public void Apply()
        {
            foreach (var register in _registers)
            {
                _bus = register.Apply(_bus);
            }
        }
    }
}