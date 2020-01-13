using System;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.DataTypes.Factories
{
    public class ByteFactory : IByteFactory
    {
        private readonly IBitFactory _bitFactory;
        private readonly IBase10Converter _base10Converter;

        public ByteFactory(IBitFactory bitFactory, IBase10Converter base10Converter)
        {
            _bitFactory = bitFactory;
            _base10Converter = base10Converter;
        }
        
        public IByte Create(IBit[] bits)
        {
            if (bits.Length != 8)
            {
                throw new ArgumentException($"A byte must be 8 bits. input array way {bits.Length} long");
            }
            
            return new Byte(bits, _bitFactory);
        }

        public IByte Create()
        {
            return new Byte(Enumerable.Range(0, 8).Select(s => _bitFactory.Create(false)).ToArray(), _bitFactory);
        }

        public IByte Create(uint input)
        {
            var number = _base10Converter.ToBit(input).ToArray();
            var padding = Enumerable.Range(0, 8 - number.Length).Select(s => _bitFactory.Create(false)).ToList();
            padding.AddRange(number);
            return new Byte(padding.ToArray(), _bitFactory);
        }
    }
}