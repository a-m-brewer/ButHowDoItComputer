using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class Byte : IByte
    {
        private bool[] _bits;

        public Byte(bool[] bits)
        {
            if (bits.Length != 8)
                throw new ArgumentException($"A byte must be 8 bits. input array way {bits.Length} long");

            _bits = bits;
        }

        public Byte()
        {
            _bits = Enumerable.Range(0, 8).Select(s => false).ToArray();
        }

        public IEnumerator<bool> GetEnumerator()
        {
            yield return _bits[0];
            yield return _bits[1];
            yield return _bits[2];
            yield return _bits[3];
            yield return _bits[4];
            yield return _bits[5];
            yield return _bits[6];
            yield return _bits[7];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(bool item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            var falseArray = new bool[8];
            _bits = falseArray.Select(s => false).ToArray();
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
            throw new NotSupportedException();
        }

        public int Count => _bits.Length;

        public bool IsReadOnly { get; } = false;

        public int IndexOf(bool item)
        {
            throw new NotSupportedException();
        }

        public void Insert(int index, bool item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public bool this[int index]
        {
            get => _bits[index];
            set => _bits[index] = value;
        }
    }
}