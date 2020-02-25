using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes;

namespace ButHowDoItComputer.Parts
{
    public class IoBus<T> : Bus<T>
    {
        private readonly SetEnable _setEnable;

        public IoBus(SetEnable setEnable, BusMessage<T> startState) : base(startState)
        {
            _setEnable = setEnable;
        }
        
        public override void UpdateData(BusMessage<T> input)
        {
            if (_setEnable.Set)
            {
                base.UpdateData(input);
            }
        }

        public override void UpdateSubs()
        {
            if (_setEnable.Enable)
            {
                base.UpdateSubs();
            }
        }
    }
}