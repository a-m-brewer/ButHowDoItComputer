using System.Collections.Generic;

namespace ButHowDoItComputer.Codes.ASCII
{
    public static class Ascii
    {
        public static Dictionary<uint, string> Codes => new Dictionary<uint, string>
        {
            {0, Null},
            {1, StartOfHeader},
            {2, StartOfText},
            {3, EndOfText},
            {4, EndOfTransmission},
            {5, Enquiry},
            {6, Acknowledge},
            {7, Bell},
            {8, Backspace},
            {9, HorizontalTab},
            {10, LineFeed},
            {11, VerticalTab},
            {12, FormFeed},
            {13, CarriageReturn},
            {14, ShiftOut},
            {15, ShiftIn},
            {16, DataLinkEscape},
            {17, DeviceControl1},
            {18, DeviceControl2},
            {19, DeviceControl3},
            {20, DeviceControl4},
            {21, NegativeAcknowledgement},
            {22, Synchronize},
            {23, EndOfTransmissionBlock},
            {24, Cancel},
            {25, EndOfMedium},
            {26, Substitute},
            {27, Escape},
            {28, FileSeparator},
            {29, GroupSeparator},
            {30, RecordSeparator},
            {31, UnitSeparator},
            {32, " "},
            {33, "!"},
            {34, "\""},
            {35, "#"},
            {36, "$"},
            {37, "%"},
            {38, "&"},
            {39, "'"},
            {40, "("},
            {41, ")"},
            {42, "*"},
            {43, "+"},
            {44, ","},
            {45, "-"},
            {46, "."},
            {47, "/"},
            {48, "0"},
            {49, "1"},
            {50, "2"},
            {51, "3"},
            {52, "4"},
            {53, "5"},
            {54, "6"},
            {55, "7"},
            {56, "8"},
            {57, "9"},
            {58, ":"},
            {59, ";"},
            {60, "<"},
            {61, "="},
            {62, ">"},
            {63, "?"},
            {64, "@"},
            {65, "A"},
            {66, "B"},
            {67, "C"},
            {68, "D"},
            {69, "E"},
            {70, "F"},
            {71, "G"},
            {72, "H"},
            {73, "I"},
            {74, "J"},
            {75, "K"},
            {76, "L"},
            {77, "M"},
            {78, "N"},
            {79, "O"},
            {80, "P"},
            {81, "Q"},
            {82, "R"},
            {83, "S"},
            {84, "T"},
            {85, "U"},
            {86, "V"},
            {87, "W"},
            {88, "X"},
            {89, "Y"},
            {90, "Z"},
            {91, "["},
            {92, "\\"},
            {93, "]"},
            {94, "^"},
            {95, "_"},
            {96, "`"},
            {97, "a"},
            {98, "b"},
            {99, "c"},
            {100, "d"},
            {101, "e"},
            {102, "f"},
            {103, "g"},
            {104, "h"},
            {105, "i"},
            {106, "j"},
            {107, "k"},
            {108, "l"},
            {109, "m"},
            {110, "n"},
            {111, "o"},
            {112, "p"},
            {113, "q"},
            {114, "r"},
            {115, "s"},
            {116, "t"},
            {117, "u"},
            {118, "v"},
            {119, "w"},
            {120, "x"},
            {121, "y"},
            {122, "z"},
            {123, "{"},
            {124, "|"},
            {125, "}"},
            {126, "~"},
            {127, Delete}
        };

        public static string Null => "\0";
        public static string StartOfHeader => "\u0001";
        public static string StartOfText => "\u0002";
        public static string EndOfText => "\u0003";
        public static string EndOfTransmission => "\u0004";
        public static string Enquiry => "\u0005";
        public static string Acknowledge => "\u0006";
        public static string Bell => "\a";
        public static string Backspace => "\b";
        public static string HorizontalTab => "\t";
        public static string LineFeed => "\n";
        public static string VerticalTab => "\v";
        public static string FormFeed => "\f";
        public static string CarriageReturn => "\r";
        public static string ShiftOut => "\u000e";
        public static string ShiftIn => "\u000f";
        public static string DataLinkEscape => "\u0010";
        public static string DeviceControl1 => "\u0011";
        public static string DeviceControl2 => "\u0012";
        public static string DeviceControl3 => "\u0013";
        public static string DeviceControl4 => "\u0014";
        public static string NegativeAcknowledgement => "\u0015";
        public static string Synchronize => "\u0016";
        public static string EndOfTransmissionBlock => "\u0017";
        public static string Cancel => "\u0018";
        public static string EndOfMedium => "\u0019";
        public static string Substitute => "\u001a";
        public static string Escape => "\u001b";
        public static string FileSeparator => "\u001c";
        public static string GroupSeparator => "\u001d";
        public static string RecordSeparator => "\u001f";
        public static string UnitSeparator => "US";
        public static string Delete => "\u0021";
    }
}