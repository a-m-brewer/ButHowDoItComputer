using System.Collections.Generic;

namespace ButHowDoItComputer.DataTypes.Interfaces
{
    public interface IByte : IList<IBit>
    {
        public IBit One { get; set; }
        public IBit Two { get; set; }
        public IBit Three { get; set; }
        public IBit Four { get; set; }
        public IBit Five { get; set; }
        public IBit Six { get; set; }
        public IBit Seven { get; set; }
        public IBit Eight { get; set; }
    }
}