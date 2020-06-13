using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ButHowDoItComputer.DataTypes
{
    public class StepperOutput : IList<bool>
    {
        private bool[] _bits;

        public StepperOutput()
        {
            _bits = new bool[7];
        }

        public StepperOutput(bool[] bits)
        {
            if (bits.Length != 7)
                throw new ArgumentException($"A stepper has 7 inputs. input array way {bits.Length} long");

            _bits = bits;
        }

        public IEnumerator<bool> GetEnumerator()
        {
            return ((IEnumerable<bool>) _bits).GetEnumerator();
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
            _bits = new bool[8];
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
        public bool IsReadOnly => false;

        public int IndexOf(bool item)
        {
            return _bits.ToList().IndexOf(item);
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