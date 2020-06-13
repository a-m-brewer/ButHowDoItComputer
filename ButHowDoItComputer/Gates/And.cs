using System;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class And : IAnd
    {
        public bool Apply(params bool[] bits)
        {
            return Array.TrueForAll(bits, b => b);
        }
    }
}