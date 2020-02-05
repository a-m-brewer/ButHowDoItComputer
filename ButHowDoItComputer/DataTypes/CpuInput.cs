using System;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class CpuInput
    {
        public Func<IByte> Ir { get; set; }
        public Func<Caez> Caez { get; set; }
    }
}