using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class MemoryGate : IMemoryGate
    {
        private readonly INAnd _nAnd;
        private bool _c;
        private bool _o;

        public MemoryGate(INAnd nAnd)
        {
            _nAnd = nAnd;
            _o = false;
            _c = false;
        }

        public bool Apply(bool input, bool set)
        {
            var a = _nAnd.Apply(input, set);
            var b = _nAnd.Apply(a, set);

            _c = _nAnd.Apply(b, _o);
            _o = _nAnd.Apply(a, _c);

            return _o;
        }
    }
}