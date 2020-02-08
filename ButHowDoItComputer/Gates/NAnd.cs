using System.Collections.Generic;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class NAnd : INAnd
    {
        private readonly INot _not;
        private readonly IAnd _and;

        public NAnd(INot not, IAnd and)
        {
            _not = not;
            _and = and;
        }
        
        public bool Apply(params bool[] bits)
        {
            return _not.Apply(_and.Apply(bits));
        }
    }
}