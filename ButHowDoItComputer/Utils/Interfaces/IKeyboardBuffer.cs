using System.Threading;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Utils.Interfaces
{
    public interface IKeyboardBuffer<out TBusDataType> where TBusDataType : IBusDataType
    {
        TBusDataType Pop();
        void Clear();
        void Start(CancellationToken cancellationToken);
        void Get();
    }
}