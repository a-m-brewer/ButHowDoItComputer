using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Not : INot
    {
        private readonly IBitFactory _bitFactory;

        public Not(IBitFactory bitFactory)
        {
            _bitFactory = bitFactory;
        }
        
        public IBit Apply(IBit bit)
        {
            return _bitFactory.Create(!bit.State);
        }
    }
}