using System;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes.Factories
{
    public class ByteFactory : IByteFactory
    {
        private readonly IBitFactory _bitFactory;

        public ByteFactory(IBitFactory bitFactory)
        {
            _bitFactory = bitFactory;
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
    }
}