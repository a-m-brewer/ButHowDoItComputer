using System.Collections.Generic;

namespace ButHowDoItComputer.DataTypes
{
    public class AluOutput<TBusDataType> where TBusDataType : IList<bool>
    {
        public TBusDataType Output { get; set; }
        public bool ALarger { get; set; }
        public bool Equal { get; set; }
        public bool CarryOut { get; set; }
        public bool Zero { get; set; }
    }
}