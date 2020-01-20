using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class StepperOutput : IList<IBit>
    {
        private readonly IBitFactory _bitFactory;
        private IBit[] _bits;

        public StepperOutput(IBitFactory bitFactory)
        {
            _bitFactory = bitFactory;
            _bits = Enumerable.Range(0, 7).Select(_ => _bitFactory.Create(false)).ToArray();
        }

        public StepperOutput(IBit[] bits, IBitFactory bitFactory)
        {
            _bitFactory = bitFactory;
            if (bits.Length != 7)
            {
                throw new ArgumentException($"A stepper has 7 inputs. input array way {bits.Length} long");
            }

            _bits = bits;
        }

        public IEnumerator<IBit> GetEnumerator()
        {
            return ((IEnumerable<IBit>) _bits).GetEnumerator();
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
        public bool IsReadOnly => false;
        
        public int IndexOf(IBit item)
        {
            return _bits.ToList().IndexOf(item);
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