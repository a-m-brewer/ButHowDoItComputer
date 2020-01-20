using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class Byte : IByte
    {
        private IBit[] _bits;
        private readonly IBitFactory _bitFactory;

        public Byte(IBit[] bits, IBitFactory bitFactory)
        {
            if (bits.Length != 8)
            {
                throw new ArgumentException($"A byte must be 8 bits. input array way {bits.Length} long");
            }

            _bits = bits;
            _bitFactory = bitFactory;
        }

        public Byte(IBitFactory bitFactory)
        {
            _bitFactory = bitFactory;
            _bits = Enumerable.Range(0, 8).Select(s => _bitFactory.Create(false)).ToArray();
        }

        public IBit One
        {
            get => _bits[0];
            set => _bits[0] = value;
        }
        
        public IBit Two
        {
            get => _bits[1];
            set => _bits[1] = value;
        }
        
        public IBit Three
        {
            get => _bits[2];
            set => _bits[2] = value;
        }
        
        public IBit Four
        {
            get => _bits[3];
            set => _bits[3] = value;
        }
        
        public IBit Five
        {
            get => _bits[4];
            set => _bits[4] = value;
        }
        
        public IBit Six
        {
            get => _bits[5];
            set => _bits[5] = value;
        }
        
        public IBit Seven
        {
            get => _bits[6];
            set => _bits[6] = value;
        }
        
        public IBit Eight
        {
            get => _bits[7];
            set => _bits[7] = value;
        }

        public IEnumerator<IBit> GetEnumerator()
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

        public void Add(IBit item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            var falseArray = new bool[8];
            _bits = falseArray.Select(s => _bitFactory.Create(s)).ToArray();
        }

        public bool Contains(IBit item)
        {
            return _bits.Contains(item);
        }

        public void CopyTo(IBit[] array, int arrayIndex)
        {
            _bits.CopyTo(array, arrayIndex);
        }

        public bool Remove(IBit item)
        {
            throw new NotSupportedException();
        }

        public int Count => _bits.Length;

        public bool IsReadOnly { get; } = false;
        
        public int IndexOf(IBit item)
        {
            throw new NotSupportedException();
        }

        public void Insert(int index, IBit item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public IBit this[int index]
        {
            get => _bits[index];
            set => _bits[index] = value;
        }
    }
}