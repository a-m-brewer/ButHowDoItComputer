using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class ByteOr : IByteOr
    {
        private readonly IOr _or;
        private readonly IByteFactory _byteFactory;

        public ByteOr(IOr or, IByteFactory byteFactory)
        {
            _or = or;
            _byteFactory = byteFactory;
        }
        
        public IByte Apply(params IByte[] input)
        {
            var groups = new List<List<bool>>();
            for (var i = 0; i < input[0].Count; i++)
            {
                var tempList = input.Select(t => t[i]).ToList();
                groups.Add(tempList);
            }

            var result = groups.Select(s => _or.Apply(s.ToArray())).ToArray();

            return _byteFactory.Create(result);
        }
    }
}