using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public static class ByteHelpers
    {
        public static bool[] ToBits(this IByte input)
        {
            return new[]
                {input[0], input[1], input[2], input[3], input[4], input[5], input[6], input[7]};
        }
    }
}