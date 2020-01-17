using ButHowDoItComputer.DataTypes;

namespace ButHowDoItComputer.Utils
{
    public static class OpCodes
    {
        public static Op Add => new Op { One = false.ToBit(), Two = false.ToBit(), Three = false.ToBit() };
        public static Op Shr => new Op { One = false.ToBit(), Two = false.ToBit(), Three = true.ToBit() };
        public static Op Shl => new Op { One = false.ToBit(), Two = true.ToBit(), Three = false.ToBit() };
        public static Op Not => new Op { One = false.ToBit(), Two = true.ToBit(), Three = true.ToBit() };
        public static Op And => new Op { One = true.ToBit(), Two = false.ToBit(), Three = false.ToBit() };
        public static Op Or => new Op { One = true.ToBit(), Two = false.ToBit(), Three = true.ToBit() };
        public static Op XOr => new Op { One = true.ToBit(), Two = true.ToBit(), Three = false.ToBit() };
        public static Op Cmp => new Op { One = true.ToBit(), Two = true.ToBit(), Three = true.ToBit() };
    }
}
