using System.Collections;
using System.Collections.Generic;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.DataTypes
{
    public class CpuEnables : IEnumerable<ICpuSubscriberNotifier<bool>>
    {
        public ICpuSubscriberNotifier<bool> Ram { get; set; }

        // From what I can tell ACC is just a register to store the result of ALU pg. 91
        public ICpuSubscriberNotifier<bool> Acc { get; set; }
        public ICpuSubscriberNotifier<bool> R0 { get; set; }
        public ICpuSubscriberNotifier<bool> R1 { get; set; }
        public ICpuSubscriberNotifier<bool> R2 { get; set; }
        public ICpuSubscriberNotifier<bool> R3 { get; set; }

        public ICpuSubscriberNotifier<bool> Iar { get; set; }

        public ICpuSubscriberNotifier<bool> IoClk { get; set; }
        public ICpuSubscriberNotifier<bool> InputOutput { get; set; }
        public ICpuSubscriberNotifier<bool> DataAddress { get; set; }

        public IEnumerator<ICpuSubscriberNotifier<bool>> GetEnumerator()
        {
            yield return Ram;
            yield return Acc;
            yield return R0;
            yield return R1;
            yield return R2;
            yield return R3;
            yield return Iar;
            yield return IoClk;
            yield return InputOutput;
            yield return DataAddress;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}