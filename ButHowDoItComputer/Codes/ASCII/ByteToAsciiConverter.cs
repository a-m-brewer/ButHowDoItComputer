using System;
using ButHowDoItComputer.Codes.ASCII.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Codes.ASCII
{
    public class ByteToAsciiConverter
    {
        private readonly IByteToBase10Converter _byteToBase10Converter;

        public ByteToAsciiConverter(IByteToBase10Converter byteToBase10Converter)
        {
            _byteToBase10Converter = byteToBase10Converter;
        }
        
        public string ToAscii(IByte input)
        {
            var intInput = _byteToBase10Converter.ToInt(input);
            return Ascii.Codes[intInput];
        }
    }
}