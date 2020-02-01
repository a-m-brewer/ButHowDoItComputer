using System.Collections;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class Caez : IEnumerable<IBit>
    {
        public IBit C { get; set; }
        public IBit A { get; set; }
        public IBit E { get; set; }
        public IBit Z { get; set; }
        
        public IEnumerator<IBit> GetEnumerator()
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