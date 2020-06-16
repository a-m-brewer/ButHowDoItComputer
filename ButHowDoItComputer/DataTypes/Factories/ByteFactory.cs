using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;
using Byte = ButHowDoItComputer.DataTypes.BusDataTypes.Byte;

namespace ButHowDoItComputer.DataTypes.Factories
{
    public class ByteFactory : IByteFactory
    {
        private readonly IBase10Converter _base10Converter;

        public ByteFactory(IBase10Converter base10Converter)
        {
            _base10Converter = base10Converter;
        }

        public IByte CreateParams(params bool[] bits)
        {
            return Create(bits);
        }

        public IByte Create(IList<bool> bits)
        {
            if (bits.Count != 8)
                throw new ArgumentException($"A byte must be 8 bits. input array way {bits.Count} long");

            return new Byte(bits);
        }

        public IByte Create()
        {
            return new Byte(Enumerable.Range(0, 8).Select(s => false).ToArray());
        }

        public IByte Create(uint input)
        {
            var number = _base10Converter.ToBit(input);
            var padding = new bool[8 - number.Count];
            number.AddRange(padding);
            return new Byte(number);
        }
    }
}