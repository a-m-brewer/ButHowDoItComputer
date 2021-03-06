using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public class Base10Converter : IBase10Converter
    {
        public IEnumerable<bool> ToBit(uint input)
        {
            var quotient = (int) input;

            var result = new List<bool>();

            while (quotient != 0)
            {
                quotient = Math.DivRem(quotient, 2, out var remainder);
                var state = remainder == 1;
                result.Add(state);
            }

            return result;
        }

        public uint ToInt(IList<bool> bits)
        {
            if (!bits.Any()) return 0;

            var total = 0;
            for (var i = 0; i < bits.Count; i++)
            {
                if (!bits[i]) continue;
                total += (int) Math.Pow(2, i);
            }

            return (uint) total;
        }

        public IEnumerable<bool> Pad(List<bool> bits, int amount)
        {
            var toAdd = amount - bits.Count;
            bits.AddRange(toAdd.BitListOfLength());
            return bits;
        }
    }
}