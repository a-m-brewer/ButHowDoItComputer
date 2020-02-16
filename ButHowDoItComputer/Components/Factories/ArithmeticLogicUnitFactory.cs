using System;
using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Components.Factories
{
    public class ArithmeticLogicUnitFactory : IArithmeticLogicUnitFactory
    {
        public IArithmeticLogicUnit Create()
        {
            return Create(b => {}, caez => {});
        }

        public IArithmeticLogicUnit Create(Action<IByte> updateAcc, Action<Caez> updateFlags)
        {
            var not = new Not();
            var and = new And();
            var nAnd = new NAnd(not, and);
            var or = new Or(not, nAnd);
            var xOr = new XOr(not, nAnd);
            var byteFactory = new ByteFactory(new Base10Converter());
            return new ArithmeticLogicUnit(
                new ByteXOr(xOr, byteFactory),
                new ByteOr(or, byteFactory),
                new ByteAnd(and, byteFactory),
                new Inverter(not, byteFactory),
                new ByteAdder(new BitAdder(xOr, or, and), byteFactory),
                new ByteEnabler(and, byteFactory),
                and,
                new IsZeroGate(or, not),
                new ByteDecoder(new Decoder(not, and), byteFactory),
                new ByteRightShifter(byteFactory),
                new ByteLeftShifter(byteFactory),
                or,
                new AluWire(byteFactory),
                new ByteComparator(new BitComparator(xOr, and, or, not), byteFactory), updateFlags,
                updateAcc, byteFactory);
        }
    }

    public interface IArithmeticLogicUnitFactory : IObjectCreationFactory<IArithmeticLogicUnit>
    {
        IArithmeticLogicUnit Create(Action<IByte> updateAcc, Action<Caez> updateFlags);
    }
}