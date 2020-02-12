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

        public bool Apply(params bool[] bits)
        {
            var negatedBits = bits.Select(s => _not.Apply(s)).ToArray();
            return _nAnd.Apply(negatedBits);
        }
    }
}