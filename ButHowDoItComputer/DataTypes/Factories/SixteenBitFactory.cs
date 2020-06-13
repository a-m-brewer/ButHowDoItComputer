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
            return CreateParams(new bool[16]);
        }

        public ISixteenBit CreateParams(params bool[] bits)
        {
            return Create(bits);
        }

        public ISixteenBit Create(IList<bool> bits)
        {
            if (bits.Count > 16)
                throw new ArgumentException($"SixteenBit must be 16 long. input array was {bits.Count} long");

            return new SixteenBit(PadBits(bits));
        }

        public ISixteenBit Create(uint input)
        {
            var number = _base10Converter.ToBit(input);
            var output = PadBits(number);
            return new SixteenBit(output);
        }

        private static IList<bool> PadBits(IList<bool> input)
        {
            if (input.Count >= 16)
            {
                return input;
            }
            
            var padding = new bool[16 - input.Count];
            var tempList = new List<bool>(input.Count + padding.Length);
            tempList.AddRange(input);
            tempList.AddRange(padding);
            return tempList;
        }
    }
}