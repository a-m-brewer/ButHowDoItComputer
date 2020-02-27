using System;
using ButHowDoItComputer.Codes.ASCII;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ui = new UserInput(new ConsoleInputDevice(), 
                new ByteToAsciiConverter(new ByteToBase10Converter(new ByteFactory(new Base10Converter()),
                    new Base10Converter())));

            var bytes = ui.Input();
        }
    }
}