using ButHowDoItComputer.DataTypes;

namespace ButHowDoItComputer.Utils
{
    public static class OpCodes
    {
        public static Op Add => new Op { One = false, Two = false, Three = false };
        public static Op Shr => new Op { One = false, Two = true, Three = false };
        public static Op Shl => new Op { One = false, Two = false, Three = true };
        public static Op Not => new Op { One = false, Two = true, Three = true };
        public static Op And => new Op { One = true, Two = false, Three = false };
        public static Op Or => new Op { One = true, Two = false, Three = true };
        public static Op XOr => new Op { One = true, Two = true, Three = false };
        public static Op Cmp => new Op { One = true, Two = true, Three = true };
    }
}
