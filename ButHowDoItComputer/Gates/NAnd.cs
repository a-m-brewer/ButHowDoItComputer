using System.Collections.Generic;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class NAnd : INAnd
    {
        private readonly IAnd _and;
        private readonly INot _not;

        public NAnd(INot not, IAnd and)
        {
            _not = not;
            _and = and;
        }

        public bool ApplyParams(params bool[] bits)
        {
            return Apply(bits);
        }

        public bool Apply(IList<bool> bits)
        {
            return _not.Apply(_and.Apply(bits));
        }
    }
}