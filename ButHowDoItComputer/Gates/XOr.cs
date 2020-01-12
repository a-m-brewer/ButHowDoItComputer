using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class XOr : IXOr
    {
        private readonly INot _not;
        private readonly INAnd _nAnd;

        public XOr(INot not, INAnd nAnd)
        {
            _not = not;
            _nAnd = nAnd;
        }

        public IBit Apply(IEnumerable<IBit> bits)
        {
            var bitList = bits.ToList();

            if (bitList.Count < 2)
            {
                return new Bit(false);
            }

            var lastResult = ApplyXor(bitList[0], bitList[1]);

            for (var i = 2; i < bitList.Count; i++)
            {
                lastResult = ApplyXor(lastResult, bitList[i]);
            }

            return lastResult;
        }

        private IBit ApplyXor(IBit a, IBit b)
        {
            var notA = _not.Apply(a);
            var notB = _not.Apply(b);

            var nAndA = _nAnd.Apply(new List<IBit> {a, notB});
            var nAndB = _nAnd.Apply(new List<IBit> {notA, b});

            return _nAnd.Apply(new List<IBit> {nAndA, nAndB});
        }
    }
}