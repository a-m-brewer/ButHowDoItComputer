using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public class Base10Converter : IBase10Converter
    {
        private readonly IBitFactory _bitFactory;

        public Base10Converter(IBitFactory bitFactory)
        {
            _bitFactory = bitFactory;
        }
        
        public IEnumerable<IBit> ToBit(uint input)
        {
            var quotient = (int)input;
            
            var result = new List<IBit>();
            
            while (quotient != 0)
            {
                quotient = Math.DivRem(quotient, 2, out var remainder);
                var state = remainder == 1;
                result.Add(_bitFactory.Create(state));
            }

            return result;
        }

        public uint ToInt(IList<IBit> bits)
        {
            if (!bits.Any())
            {
                return 0;
            }
            
            var total = 0;
            for (var i = 0; i < bits.Count; i++)
            {
                if(!bits[i].State) continue;
                total += (int) Math.Pow(2, i);
            }

            return (uint) total;
        }
    }
}