using System.Collections.Generic;
using System.Linq;

namespace ButHowDoItComputer.Utils
{
    public static class BitHelpers
    {
        public static IList<bool> Increment(this IList<bool> bits, uint amount)
        {
            var base10Converter = new Base10Converter();
            var asInt = base10Converter.ToInt(bits) + amount;
            var asBits = base10Converter.ToBit(asInt).ToList();

            return asBits;
        }

        public static IList<bool> Increment(this bool bit, uint amount)
        {
            return Increment(new List<bool> {bit}, amount);
        }

        public static bool[] BitListOfLength(this int size)
        {
            return Enumerable.Range(0, size).Select(s => false).ToArray();
        }
    }
}