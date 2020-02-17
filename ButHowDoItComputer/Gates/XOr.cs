using System.Linq;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class XOr : IXOr
    {
        private readonly INAnd _nAnd;
        private readonly INot _not;

        public XOr(INot not, INAnd nAnd)
        {
            _not = not;
            _nAnd = nAnd;
        }

        public bool Apply(params bool[] bits)
        {
            var bitList = bits.ToList();

            if (bitList.Count < 2) return false;

            var lastResult = ApplyXor(bitList[0], bitList[1]);

            for (var i = 2; i < bitList.Count; i++) lastResult = ApplyXor(lastResult, bitList[i]);

            return lastResult;
        }

        private bool ApplyXor(bool a, bool b)
        {
            var notA = _not.Apply(a);
            var notB = _not.Apply(b);

            var nAndA = _nAnd.Apply(a, notB);
            var nAndB = _nAnd.Apply(notA, b);

            return _nAnd.Apply(nAndA, nAndB);
        }
    }
}