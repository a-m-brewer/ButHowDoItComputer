using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class CaezEnabler : ICaezEnabler
    {
        public Caez Apply(Caez input, bool set)
        {
            return new Caez
            {
                C = input.C && set,
                A = input.A && set,
                E = input.E && set,
                Z = input.Z && set
            };
        }
    }
}