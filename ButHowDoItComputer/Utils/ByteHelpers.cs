using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public static class ByteHelpers
    {
        public static bool[] ToBits<TBusDataType>(this TBusDataType input) where TBusDataType : IList<bool>
        {
            var tmp = new bool[input.Count];

            for (var i = 0; i < tmp.Length; i++)
            {
                tmp[i] = input[i];
            }

            return tmp;
        }
    }
}