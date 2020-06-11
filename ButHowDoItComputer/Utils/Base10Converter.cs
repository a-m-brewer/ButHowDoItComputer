using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public class Base10Converter : IBase10Converter
    {
        public List<bool> ToBit(uint input)
        {
            var quotient = (int) input;

            var result = new List<bool>();

            while (quotient != 0)
            {
                var (tmpQuotient, newState) = UpdateQuotient(quotient);
                quotient = tmpQuotient;
                result.Add(newState);
            }

            return result;
        }

        public bool[] ToBit(uint input, int arraySize)
        {
            var quotient = (int) input;

            var result = new bool[arraySize];

            for (var i = 0; i < result.Length; i++)
            {
                if (quotient <= 0)
                {
                    break;
                }

                var (tmpQuotient, newState) = UpdateQuotient(quotient);
                quotient = tmpQuotient;
                result[i] = newState;
            }

            return result;
        }

        private (int quotient, bool state) UpdateQuotient(int quotient)
        {
            var quotientTemp = Math.DivRem(quotient, 2, out var remainder);
            var state = remainder == 1;
            return (quotientTemp, state);
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
            bits.AddRange(new bool[toAdd]);
            return bits;
        }
    }
}