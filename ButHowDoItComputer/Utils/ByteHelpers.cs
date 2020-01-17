using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public static class ByteHelpers
    {
        public static IBit[] ToBits(this IByte input)
        {
            return new[] { input.One, input.Two, input.Three, input.Four, input.Five, input.Six, input.Seven, input.Eight };
        }
    }
}
