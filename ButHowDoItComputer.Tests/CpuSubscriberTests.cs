﻿using ButHowDoItComputer.Components.CpuSubscribers;
using ButHowDoItComputer.DataTypes;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class CpuSubscriberTests
    {
        // I know this is testing csharp more than the code but I just wanted it for sanity
        [Test]
        public void HavingSamePartInMultipleSubscriberObjectsStillUpdatesState()
        {
            var register = TestUtils.CreateRegister();
            var enableSubNotifier = new EnableSubscriberNotifier(register);
            var setSubNotifier = new SetSubscriberNotifier(register);
            
            enableSubNotifier.Update(new Bit(true));
            setSubNotifier.Update(new Bit(true));
            
            Assert.True(register.Enable.State);
            Assert.True(register.Set.State);
        }

        // same here
        [Test]
        public void WorksWhenIsAssignedToOtherComplexObject()
        {
            var register = TestUtils.CreateRegister();
            var enableSubNotifier = new EnableSubscriberNotifier(register);
            var setSubNotifier = new SetSubscriberNotifier(register);

            var cpuEnables = new CpuEnables {R0 = enableSubNotifier};
            var cpuSets = new CpuSets {R0 = setSubNotifier};
            cpuEnables.R0.Update(new Bit(true));
            cpuSets.R0.Update(new Bit(true));
            
            Assert.True(register.Enable.State);
            Assert.True(register.Set.State);
        }
    }
}