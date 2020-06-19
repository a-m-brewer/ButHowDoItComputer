using System.Collections.Generic;
using System.Threading;

namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface IKeyboardBuffer<out TBusDataType> where TBusDataType : IList<bool>
    {
        TBusDataType Pop();
        void Clear();
        void Start(CancellationToken cancellationToken);
        void Get();
    }
}