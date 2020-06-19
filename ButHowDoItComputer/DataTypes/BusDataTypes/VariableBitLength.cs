using System;
using System.Collections;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes.BusDataTypes
{
    public class VariableBitLength : IList<bool>
    {
        private IList<bool> _bits;
        private int _expectedLength;

        public VariableBitLength(IList<bool> bits, int expectedLength)
        {
            if (bits.Count < 8)
                throw new ArgumentException($"{nameof(bits)} computer is a minimum on 8-bits. input array way {bits.Count} long");

            if (expectedLength < 8)
            {
                throw new ArgumentException($"{nameof(expectedLength)} must be greater than 8: was {expectedLength}");
            }

            _bits = bits;
            _expectedLength = expectedLength;
        }

        public VariableBitLength(IList<bool> bits) : this(bits, 8)
        {
        }

        public VariableBitLength()
        {
            _expectedLength = 8;
            Clear();
        }
        
        public IEnumerator<bool> GetEnumerator()
        {
            return _bits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(bool item)
        {
            _bits.Add(item);
        }

        public void Clear()
        {
            _bits = new bool[_expectedLength];
        }

        public bool Contains(bool item)
        {
            return _bits.Contains(item);
        }

        public void CopyTo(bool[] array, int arrayIndex)
        {
            _bits.CopyTo(array, arrayIndex);
        }

        public bool Remove(bool item)
        {
            if (_bits.Count - 1 < 8)
            {
                throw new ArgumentException("Length must not go bellow 8");
            }
            
            return _bits.Remove(item);
        }

        public int Count => _bits.Count;
        public bool IsReadOnly => _bits.IsReadOnly;
        
        public int IndexOf(bool item)
        {
            return _bits.IndexOf(item);
        }

        public void Insert(int index, bool item)
        {
            _bits.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            if (_bits.Count - 1 < 8)
            {
                throw new ArgumentException("Length must not go bellow 8");
            }
            
            _bits.RemoveAt(index);
        }

        public bool this[int index]
        {
            get => _bits[index];
            set => _bits[index] = value;
        }
    }
}