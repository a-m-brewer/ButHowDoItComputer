using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public ISixteenBit Create()
        {
            return Create(Enumerable.Range(0, 16).Select(s => false).ToArray());
        }

        public ISixteenBit Create(params bool[] bits)
        {
            if (bits.Length > 16)
                throw new ArgumentException($"SixteenBit must be 16 long. input array was {bits.Length} long");

            return new SixteenBit(PadBits(bits.ToList()));
        }

        public ISixteenBit Create(uint input)
        {
            var number = _base10Converter.ToBit(input).ToList();
            var output = PadBits(number);
            return new SixteenBit(output);
        }

        private static bool[] PadBits(List<bool> input)
        {
            var padding = Enumerable.Range(0, 16 - input.Count).Select(s => false).ToList();
            input.AddRange(padding);
            return input.ToArray();
        }
    }
}