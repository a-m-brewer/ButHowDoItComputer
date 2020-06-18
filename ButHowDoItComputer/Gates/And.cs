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
            for (var i = 0; i < bits.Count; i++)
            {
                if (!bits[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}