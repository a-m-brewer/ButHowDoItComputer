using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Not : INot
    {
        public bool Apply(bool bit)
        {
            return !bit;
        }
    }
}