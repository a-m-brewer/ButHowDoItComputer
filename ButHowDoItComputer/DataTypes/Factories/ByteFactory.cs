using System;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.DataTypes.Factories
{
    public class ByteFactory : IByteFactory
    {
        private readonly IBase10Converter _base10Converter;

        public ByteFactory(IBase10Converter base10Converter)
        {
            _base10Converter = base10Converter;
        }

        public IList<bool> CreateParams(params bool[] bits)
        {
            return Create(bits);
        }

        public IList<bool> Create(IList<bool> bits)
        {
            if (bits.Count != 8)
                throw new ArgumentException($"A byte must be 8 bits. input array way {bits.Count} long");

            return bits;
        }

        public IList<bool> Create()
        {
            return new bool[8];
        }

        public IList<bool> Create(uint input)
        {
            var number = _base10Converter.ToBit(input);
            var padding = new bool[8 - number.Count];
            number.AddRange(padding);
            return number;
        }
    }
}