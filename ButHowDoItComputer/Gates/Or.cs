using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Or : IOr
    {
        private readonly INAnd _nAnd;
        private readonly INot _not;

        public Or(INot not, INAnd nAnd)
        {
            _not = not;
            _nAnd = nAnd;
        }

        public bool ApplyParams(params bool[] bits)
        {
            return Apply(bits);
        }

        public bool Apply(IList<bool> bits)
        {
            var negatedBits = new bool[bits.Count];

            for (var i = 0; i < negatedBits.Length; i++)
            {
                negatedBits[i] = _not.Apply(bits[i]);
            }
            
            return _nAnd.Apply(negatedBits);
        }
    }
}