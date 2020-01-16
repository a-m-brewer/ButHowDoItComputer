using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Or : IOr
    {
        private readonly INot _not;
        private readonly INAnd _nAnd;

        public Or(INot not, INAnd nAnd)
        {
            _not = not;
            _nAnd = nAnd;
        }

        public IBit Apply(params IBit[] bits)
        {
            var negatedBits = bits.Select(s => _not.Apply(s)).ToArray();
            return _nAnd.Apply(negatedBits);
        }
    }
}