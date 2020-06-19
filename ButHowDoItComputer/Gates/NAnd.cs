using System.Collections.Generic;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Gates
{
    public class NAnd : INAnd
    {
        private readonly INot _not;

        public NAnd(INot not)
        {
            _not = not;
        }

        public bool ApplyParams(params bool[] bits)
        {
            return Apply(bits);
        }

        public bool Apply(IList<bool> bits)
        {
            return _not.Apply(bits.AndList());
        }
    }
}