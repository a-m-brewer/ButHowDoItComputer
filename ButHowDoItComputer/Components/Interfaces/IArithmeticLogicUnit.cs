﻿using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Components.Interfaces
{
    public interface IArithmeticLogicUnit
    {
        AluOutput Apply(IByte a, IByte b, IBit carryIn, Op op);
    }
}