﻿using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Adapters.Interfaces
{
    public interface IByteGateToListFactory
    {
        IByteListGate Convert(IByteGate byteGate, IBit set);
    }
}
