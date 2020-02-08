using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class And : IAnd
    {
        public bool Apply(params bool[] bits)
        {
            return bits.All(a => a);
        }
    }
}