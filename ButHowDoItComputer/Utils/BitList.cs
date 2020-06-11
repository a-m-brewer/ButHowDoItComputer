using System.Collections.Generic;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public class BitList : List<bool>
    {
        public BitList(IEnumerable<bool> bits) : base(bits)
        {
        }
        
        private static IBase10Converter Converter { get; } = new Base10Converter();
        
        public static BitList operator ++(BitList list)
        {
            var incremented = list.Increment(1);
            var bitList = new BitList(incremented);
            return bitList;
        }

        public static bool operator <(BitList list, int value)
        {
            return Converter.ToInt(list) < value;
        }

        public static bool operator >(BitList list, int value)
        {
            return Converter.ToInt(list) > value;
        }
    }
}