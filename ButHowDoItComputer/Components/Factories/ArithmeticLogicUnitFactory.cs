using ButHowDoItComputer.Components.Interfaces;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Components.Factories
{
    public class ArithmeticLogicUnitFactory : IObjectCreationFactory<IArithmeticLogicUnit>
    {
        public IArithmeticLogicUnit Create()
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
                new Wire(byteFactory),
                new ByteComparator(new BitComparator(xOr, and, or, not), byteFactory));
        }
    }
}