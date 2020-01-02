using System;
using System.Linq;
using ButHowDoItComputer.Codes.ASCII.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Codes.ASCII
{
    public class ByteToBase10Converter : IByteToBase10Converter
    {
        private readonly IBitFactory _bitFactory;
        private readonly IByteFactory _byteFactory;

        public ByteToBase10Converter(IBitFactory bitFactory, IByteFactory byteFactory)
        {
            _bitFactory = bitFactory;
            _byteFactory = byteFactory;
        }
        
        public int ToInt(IByte input)
        {
            var total = 0;
            for (var i = 0; i < input.Count; i++)
            {
                if(!input[i].State) continue;
                total += (int) Math.Pow(2, i);
            }

            return total;
        }

        public IByte ToByte(int input)
        {
            if (255 < input || input < 0)
            {
                throw new OutOfMemoryException("Number must be between 0 and 255");
            }
            
            var bits = Enumerable.Range(0, 8).Select(_ => _bitFactory.Create(false)).ToArray();
            var quotient = input;

            var index = 0;
            
            while (quotient != 0)
            {
                quotient = Math.DivRem(quotient, 2, out var remainder);

                var state = remainder == 1;

                bits[index] = _bitFactory.Create(state);
                index++;
            }

            return _byteFactory.Create(bits);
        }
    }
}