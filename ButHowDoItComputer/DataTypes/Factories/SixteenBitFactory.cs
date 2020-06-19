using System;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.DataTypes.Factories
{
    public class SixteenBitFactory : ISixteenBitFactory
    {
        private readonly IBase10Converter _base10Converter;

        public SixteenBitFactory(IBase10Converter base10Converter)
        {
            _base10Converter = base10Converter;
        }
        
        public IList<bool> Create()
        {
            return new bool[16];
        }

        public IList<bool> CreateParams(params bool[] bits)
        {
            return Create(bits);
        }

        public IList<bool> Create(IList<bool> bits)
        {
            if (bits.Count > 16)
                throw new ArgumentException($"SixteenBit must be 16 long. input array was {bits.Count} long");
            return bits;
        }

        public IList<bool> Create(uint input)
        {
            var number = _base10Converter.ToBit(input, 16);
            return number;
        }
    }
}