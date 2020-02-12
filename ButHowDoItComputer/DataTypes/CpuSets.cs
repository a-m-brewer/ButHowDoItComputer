using System.Collections;
using System.Collections.Generic;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class CpuSets : IEnumerable<ICpuSubscriberNotifier<bool>>
    {
        public ICpuSubscriberNotifier<bool> Mar { get; set; }

        // From what I can tell ACC is just a register to store the result of ALU pg. 91
        public ICpuSubscriberNotifier<bool> Acc { get; set; }

        public ICpuSubscriberNotifier<bool> Ram { get; set; }

        // From what I can tell this is just a register with enable on that sets bus1 pg. 91
        public ICpuSubscriberNotifier<bool> Tmp { get; set; }
        public ICpuSubscriberNotifier<bool> R0 { get; set; }
        public ICpuSubscriberNotifier<bool> R1 { get; set; }
        public ICpuSubscriberNotifier<bool> R2 { get; set; }
        public ICpuSubscriberNotifier<bool> R3 { get; set; }

        public ICpuSubscriberNotifier<bool> Ir { get; set; }
        public ICpuSubscriberNotifier<bool> Iar { get; set; }
        public ICpuSubscriberNotifier<bool> IoClk { get; set; }
        public ICpuSubscriberNotifier<bool> Flags { get; set; }

        public IEnumerator<ICpuSubscriberNotifier<bool>> GetEnumerator()
        {
            yield return Mar;
            yield return Ram;
            yield return Tmp;
            yield return R0;
            yield return R1;
            yield return R2;
            yield return R3;
            yield return Acc;
            yield return Ir;
            yield return Iar;
            yield return IoClk;
            yield return Flags;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}