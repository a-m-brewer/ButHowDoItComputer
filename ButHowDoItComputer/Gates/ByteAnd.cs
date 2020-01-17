using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class ByteAnd : IByteAnd
    {
        private readonly IAnd _and;
        private readonly IByteFactory _byteFactory;

        public ByteAnd(IAnd and, IByteFactory byteFactory)
        {
            _and = and;
            _byteFactory = byteFactory;
        }
        
        public IByte Apply(params IByte[] input)
        {
            var groups = new List<List<IBit>>();
            for (var i = 0; i < input[0].Count; i++)
            {
                var tempList = input.Select(t => t[i]).ToList();
                groups.Add(tempList);
            }

            var result = groups.Select(s => _and.Apply(s.ToArray())).ToArray();

            return _byteFactory.Create(result);
        }
    }
}