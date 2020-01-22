using System.Collections;
using System.Collections.Generic;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class CpuEnables : IEnumerable<ICpuSubscriberNotifier<IBit>>
    {
        public ICpuSubscriberNotifier<IBit> Ram { get; set; }
        // From what I can tell ACC is just a register to store the result of ALU pg. 91
        public ICpuSubscriberNotifier<IBit> Acc { get; set; }
        public ICpuSubscriberNotifier<IBit> R0 { get; set; }
        public ICpuSubscriberNotifier<IBit> R1 { get; set; }
        public ICpuSubscriberNotifier<IBit> R2 { get; set; }
        public ICpuSubscriberNotifier<IBit> R3 { get; set; }
        
        public ICpuSubscriberNotifier<IBit> Iar { get; set; }
        
        public IEnumerator<ICpuSubscriberNotifier<IBit>> GetEnumerator()
        {
            yield return Ram;
            yield return Acc;
            yield return R0;
            yield return R1;
            yield return R2;
            yield return R3;
            yield return Iar;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}