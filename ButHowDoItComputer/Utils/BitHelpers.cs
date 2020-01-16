using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public static class BitHelpers
    {
        public static IList<IBit> Increment(this IList<IBit> bits, uint amount)
        {
            var base10Converter = new Base10Converter(new BitFactory());
            var asInt = base10Converter.ToInt(bits) + amount;
            var asBits = base10Converter.ToBit(asInt).ToList();

            return asBits;
        }

        public static IList<IBit> Increment(this IBit bit, uint amount)
        {
            return Increment(new List<IBit> {bit}, amount);
        }
        
        public static IBit ToBit(this bool input)
        {
            return new Bit(input);
        }
    }
}