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

        public bool One
        {
            get => _bits[0];
            set => _bits[0] = value;
        }

        public bool Two
        {
            get => _bits[1];
            set => _bits[1] = value;
        }

        public bool Three
        {
            get => _bits[2];
            set => _bits[2] = value;
        }

        public bool Four
        {
            get => _bits[3];
            set => _bits[3] = value;
        }

        public bool Five
        {
            get => _bits[4];
            set => _bits[4] = value;
        }

        public bool Six
        {
            get => _bits[5];
            set => _bits[5] = value;
        }

        public bool Seven
        {
            get => _bits[6];
            set => _bits[6] = value;
        }

        public bool Eight
        {
            get => _bits[7];
            set => _bits[7] = value;
        }

        public IEnumerator<bool> GetEnumerator()
        {
            yield return One;
            yield return Two;
            yield return Three;
            yield return Four;
            yield return Five;
            yield return Six;
            yield return Seven;
            yield return Eight;
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