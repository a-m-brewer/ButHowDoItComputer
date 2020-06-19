using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public class ByteToBase10Converter : IByteToBase10Converter
    {
        private readonly IBase10Converter _base10Converter;
        private readonly IByteFactory _byteFactory;

        public ByteToBase10Converter(IByteFactory byteFactory, IBase10Converter base10Converter)
        {
            _byteFactory = byteFactory;
            _base10Converter = base10Converter;
        }

        public uint ToInt(IList<bool> input)
        {
            return _base10Converter.ToInt(input);
        }

        public IList<bool> ToByte(uint input)
        {
            if (255 < input) throw new OutOfMemoryException("Number must be between 0 and 255");

            var result = _base10Converter.ToBit(input).ToList();

            if (result.Count == 8) return _byteFactory.CreateParams(result.ToArray());

            var toAdd = 8 - result.Count;
            result.AddRange(new bool[toAdd]);

            return _byteFactory.CreateParams(result.ToArray());
        }
    }
}