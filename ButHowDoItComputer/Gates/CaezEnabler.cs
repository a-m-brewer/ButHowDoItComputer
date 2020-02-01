using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class CaezEnabler : ICaezEnabler
    {
        private readonly IAnd _and;

        public CaezEnabler(IAnd and)
        {
            _and = and;
        }
        
        public Caez Apply(Caez input, IBit set)
        {
            return new Caez
            {
                C = _and.Apply(input.C, set),
                A = _and.Apply(input.A, set),
                E = _and.Apply(input.E, set),
                Z = _and.Apply(input.Z, set)
            };
        }
    }
}