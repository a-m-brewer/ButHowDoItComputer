using System;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IRegisterFactory<T> : IObjectCreationFactory<IRegister<T>>
    {
        IRegister<T> Create(Action<T> updateOutput, string name); 
    }
}