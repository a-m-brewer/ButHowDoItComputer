using System.Collections;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class Caez : IEnumerable<bool>
    {
        public bool C { get; set; }
        public bool A { get; set; }
        public bool E { get; set; }
        public bool Z { get; set; }
        
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
    }
}