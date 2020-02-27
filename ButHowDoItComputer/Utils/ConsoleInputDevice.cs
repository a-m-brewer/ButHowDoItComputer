using System;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public class ConsoleInputDevice : IInputDevice
    {
        public string Get()
        {
            return Console.ReadLine();
        }
    }
}