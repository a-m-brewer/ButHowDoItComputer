using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ButHowDoItComputer.DataTypes
{
    public interface ISixteenBit : IList<bool> {}
    
    public class SixteenBit : ISixteenBit
    {
        private bool[] _bits;

        public SixteenBit(bool[] bits)
        {
            if (bits.Length != 16)
                throw new ArgumentException($"{nameof(bits)} should be 16 bits. input array way {bits.Length} long");

            _bits = bits;
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
            yield return _bits[8];
            yield return _bits[9];
            yield return _bits[10];
            yield return _bits[11];
            yield return _bits[12];
            yield return _bits[13];
            yield return _bits[14];
            yield return _bits[15];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(bool item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            var falseArray = new bool[16];
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
            throw new NotImplementedException();
        }

        public int Count => _bits.Length;

        public bool IsReadOnly { get; } = false;
        
        public int IndexOf(bool item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, bool item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public bool this[int index]
        {
            get => _bits[index];
            set => _bits[index] = value;
        }
    }
}