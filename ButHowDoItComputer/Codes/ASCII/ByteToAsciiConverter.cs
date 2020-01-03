using System;
using System.Linq;
using ButHowDoItComputer.Codes.ASCII.Interfaces;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Codes.ASCII
{
    public class ByteToAsciiConverter : IByteToAsciiConverter
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

        public IByte ToByte(string inputChar)
        {
            var key = Ascii.Codes.Where(w => w.Value == inputChar).Select(s => s.Key).FirstOrDefault();
            return _byteToBase10Converter.ToByte(key);
        }
    }
}