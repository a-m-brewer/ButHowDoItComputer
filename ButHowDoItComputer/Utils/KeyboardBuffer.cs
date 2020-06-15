using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Utils
{
    public class KeyboardBuffer<TBusDataType> : IKeyboardBuffer<TBusDataType> where TBusDataType : IBusDataType
    {
        private readonly IBusDataTypeFactory<TBusDataType> _busDataTypeFactory;
        private readonly IUserInput _userInput;
        private readonly ConcurrentQueue<TBusDataType> _buffer = new ConcurrentQueue<TBusDataType>();

        public KeyboardBuffer(IBusDataTypeFactory<TBusDataType> busDataTypeFactory, IUserInput userInput)
        {
            _busDataTypeFactory = busDataTypeFactory;
            _userInput = userInput;
        }

        public TBusDataType Pop()
        {
            return _buffer.TryDequeue(out var keyCode) ? keyCode : _busDataTypeFactory.Create(0);
        }

        public void Clear()
        {
            _buffer.Clear();
        }

        public void Start(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Get();
                }
            }, cancellationToken);
        }

        public void Get()
        {
            var input = _userInput.Input();

            foreach (var i in input)
            {
                _buffer.Enqueue(_busDataTypeFactory.Create(i));
            }
        }
    }
}