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
            var a = _nAnd.ApplyParams(input, set);
            var b = _nAnd.ApplyParams(a, set);

            _c = _nAnd.ApplyParams(b, _o);
            _o = _nAnd.ApplyParams(a, _c);

            return _o;
        }
    }
}