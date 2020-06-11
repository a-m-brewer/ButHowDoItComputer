using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public static class BitHelpers
    {
        private static IBase10Converter Converter { get; } = new Base10Converter();
        
        public static IList<bool> Increment(this IList<bool> bits, uint amount)
        {
            var asInt = Converter.ToInt(bits) + amount;
            var asBits = Converter.ToBit(asInt).ToList();

            return asBits;
        }

        public static IList<bool> Increment(this bool bit, uint amount)
        {
            return Increment(new List<bool> {bit}, amount);
        }
    }
}