using ButHowDoItComputer.Components;
using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Factories;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates;
using ButHowDoItComputer.Gates.Factories;
using ButHowDoItComputer.Parts;
using ButHowDoItComputer.Utils;
using NUnit.Framework;

namespace ButHowDoItComputer.Tests
{
    [TestFixture]
    public class ShifterTests
    {
        private BitFactory _bitFactory;
        private ByteFactory _byteFactory;
        private And _and;
        private MemoryGateFactory _memoryGateFactory;

        [SetUp]
        public void Setup()
        {
            _bitFactory = new BitFactory();
            _byteFactory = new ByteFactory(_bitFactory, new Base10Converter(_bitFactory));
            _and = new And(_bitFactory);
            _memoryGateFactory = new MemoryGateFactory(new NAnd(new Not(_bitFactory), _and), _bitFactory);
        }
        
        [Test]
        public void TestRightShifter()
        {
            var inputRegister = TestUtils.CreateRegister();
            inputRegister.Set = new Bit(true);
            inputRegister.Enable = new Bit(true);
            var outputRegister = TestUtils.CreateRegister();
            outputRegister.Set = new Bit(true);
            outputRegister.Enable = new Bit(true);
            
            var beforeByte = new Byte(new IBit[]
            {
                new Bit(false), 
                new Bit(true), 
                new Bit(false), 
                new Bit(false), 
                
                new Bit(false), 
                new Bit(false), 
                new Bit(true), 
                new Bit(false)
                
            },new BitFactory());
            
            var afterByte = new Byte(new IBit[]
            {
                new Bit(true), 
                new Bit(false), 
                new Bit(false), 
                new Bit(false), 
                
                new Bit(false), 
                new Bit(true), 
                new Bit(false), 
                new Bit(false)
                
            },new BitFactory());

            inputRegister.Input = beforeByte;
            
            var sut = new RightShifter(_byteFactory, _bitFactory);
            
            sut.Apply(inputRegister, outputRegister);

            for (var i = 0; i < afterByte.Count; i++)
            {
                Assert.AreEqual(afterByte[i].State, outputRegister.Output[i].State);
            }
        }
        
        [Test]
        public void TestLeftShifter()
        {
            var inputRegister = TestUtils.CreateRegister();
            inputRegister.Set = new Bit(true);
            inputRegister.Enable = new Bit(true);
            var outputRegister = TestUtils.CreateRegister();
            outputRegister.Set = new Bit(true);
            outputRegister.Enable = new Bit(true);
            
            var beforeByte = new Byte(new IBit[]
            {
                new Bit(false), 
                new Bit(true), 
                new Bit(false), 
                new Bit(false), 
                
                new Bit(false), 
                new Bit(false), 
                new Bit(true), 
                new Bit(false)
                
            },new BitFactory());
            
            var afterByte = new Byte(new IBit[]
            {
                new Bit(false), 
                new Bit(false), 
                new Bit(true), 
                new Bit(false), 
                
                new Bit(false), 
                new Bit(false), 
                new Bit(false), 
                new Bit(true)
                
            },new BitFactory());

            inputRegister.Input = beforeByte;
            
            var sut = new LeftShifter(_byteFactory, _bitFactory);
            
            sut.Apply(inputRegister, outputRegister);

            for (var i = 0; i < afterByte.Count; i++)
            {
                Assert.AreEqual(afterByte[i].State, outputRegister.Output[i].State);
            }
        }
    }
}