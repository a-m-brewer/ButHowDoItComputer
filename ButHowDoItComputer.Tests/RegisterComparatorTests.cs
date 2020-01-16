using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class RegisterComparatorTests
    {
        private RegisterComparator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new RegisterComparator(TestUtils.CreateByteComparator());
        }
        
        [Test]
        [TestCase(0U, 0U, false, true)]
        [TestCase(0U, 1U, false, false)]
        [TestCase(1U, 0U, true, false)]
        public void CanCompareBytes(uint b1, uint b2, bool aLarger, bool equal)
        {
            var a = b1.CreateRegister();
            var b = b2.CreateRegister();
            var output = TestUtils.CreateRegister();

            var (eq, al) = _sut.AreEqual(a, b, new Bit(true), new Bit(false), output);
            
            Assert.AreEqual(aLarger, al.State);
            Assert.AreEqual(equal, eq.State);
        }
    }
}