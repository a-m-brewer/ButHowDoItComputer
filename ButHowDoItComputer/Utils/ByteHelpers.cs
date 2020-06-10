using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public static class ByteHelpers
    {
        public static bool[] ToBits<TBusDataType>(this TBusDataType input) where TBusDataType : IBusDataType
        {
            return input.Select(s => s).ToArray();
        }
    }
}