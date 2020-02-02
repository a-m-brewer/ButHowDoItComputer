using System.Collections;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class CpuSets: IEnumerable<ICpuSubscriberNotifier<IBit>>
    {
        public ICpuSubscriberNotifier<IBit> Mar { get; set; }
        // From what I can tell ACC is just a register to store the result of ALU pg. 91
        public ICpuSubscriberNotifier<IBit> Acc { get; set; }
        public ICpuSubscriberNotifier<IBit> Ram { get; set; }
        // From what I can tell this is just a register with enable on that sets bus1 pg. 91
        public ICpuSubscriberNotifier<IBit> Tmp { get; set; }
        public ICpuSubscriberNotifier<IBit> R0 { get; set; }
        public ICpuSubscriberNotifier<IBit> R1 { get; set; }
        public ICpuSubscriberNotifier<IBit> R2 { get; set; }
        public ICpuSubscriberNotifier<IBit> R3 { get; set; }

        public ICpuSubscriberNotifier<IBit> Ir { get; set; }
        public ICpuSubscriberNotifier<IBit> Iar { get; set; }
        public ICpuSubscriberNotifier<IBit> IoClk { get; set; }
        public ICpuSubscriberNotifier<IBit> Flags { get; set; }
        
        public IEnumerator<ICpuSubscriberNotifier<IBit>> GetEnumerator()
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