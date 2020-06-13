using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class And : IAnd
    {
        public bool ApplyParams(params bool[] bits)
        {
            return Apply(bits);
        }

        public bool Apply(IList<bool> bits)
        {
            return bits.All(a => a);
        }
    }
}