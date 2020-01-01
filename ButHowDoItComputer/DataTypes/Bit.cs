using System.Diagnostics;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    [DebuggerDisplay("{" + nameof(State) + "}")]
    public class Bit : IBit
    {
        public Bit(bool initialState)
        {
            State = initialState;
        }
        
        public bool State { get; set; }
    }
}