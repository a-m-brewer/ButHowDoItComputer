using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ButHowDoItComputer.DataTypes
{
    public class Caez : IList<bool>
    {
        private bool[] _caez;

        public Caez()
        {
            Clear();
        }

        public Caez(bool[] caez)
        {
            _caez = caez;
        }

        public bool C
        {
            get => _caez[0];
            set => _caez[0] = value;
        }

        public bool A
        {
            get => _caez[1];
            set => _caez[1] = value;
        }

        public bool E
        {
            get => _caez[2];
            set => _caez[2] = value;
        }

        public bool Z
        {
            get => _caez[3];
            set => _caez[3] = value;
        }

        public IEnumerator<bool> GetEnumerator()
        {
            yield return C;
            yield return A;
            yield return E;
            yield return Z;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(bool item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            _caez = new bool[4];
        }

        public bool Contains(bool item)
        {
            return _caez.Contains(item);
        }

        public void CopyTo(bool[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(bool item)
        {
            throw new System.NotImplementedException();
        }

        public int Count => _caez.Length;
        public bool IsReadOnly { get; } = false;

        public int IndexOf(bool item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, bool item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public bool this[int index]
        {
            get => _caez[index];
            set => _caez[index] = value;
        }
    }
}