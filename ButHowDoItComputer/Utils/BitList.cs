using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;

namespace ButHowDoItComputer.Utils
{
    public class BitList : List<bool>
    {
        public static BitList operator ++(BitList list)
        {
            var incremented = list.Increment(1);
            var bitList = new BitList();
            bitList.AddRange(incremented);
            return bitList;
        }

        public static bool operator <(BitList list, int value)
        {
            var converter = new Base10Converter();
            return converter.ToInt(list) < value;
        }

        public static bool operator >(BitList list, int value)
        {
            var converter = new Base10Converter();
            return converter.ToInt(list) > value;
        }

    }
}