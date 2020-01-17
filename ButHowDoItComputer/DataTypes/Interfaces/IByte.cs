using System.Collections.Generic;

namespace ButHowDoItComputer.DataTypes.Interfaces
{
    public interface IByte : IList<IBit>
    {
        IBit One { get; set; }
        IBit Two { get; set; }
        IBit Three { get; set; }
        IBit Four { get; set; }
        IBit Five { get; set; }
        IBit Six { get; set; }
        IBit Seven { get; set; }
        IBit Eight { get; set; }
    }
}