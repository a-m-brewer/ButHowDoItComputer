using ButHowDoItComputer.DataTypes;
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

        public Caez Apply(Caez input, bool set)
        {
            return new Caez
            {
                C = _and.ApplyParams(input.C, set),
                A = _and.ApplyParams(input.A, set),
                E = _and.ApplyParams(input.E, set),
                Z = _and.ApplyParams(input.Z, set)
            };
        }
    }
}