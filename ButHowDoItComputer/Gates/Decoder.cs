using System;
using System.Collections.Generic;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Utils;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Decoder : IDecoder
    {
        private readonly IBase10Converter _base10Converter;
        private readonly INot _not;

        public Decoder(INot not, IBase10Converter base10Converter)
        {
            _not = not;
            _base10Converter = base10Converter;
        }

        public IList<bool> ApplyParams(params bool[] inputs)
        {
            return Apply(inputs);
        }

        public IList<bool> Apply(IList<bool> inputs)
        {
            // get a truth table based on the length of the input
            var combinations = GenerateCombinations(inputs.Count);

            // apply the not inputs based on each truth table row
            var allGatesInputs = CreateGatesInputs(combinations, inputs);

            // take the inputs and apply and to them
            var gatesOutput = new bool[allGatesInputs.Count];

            for (var i = 0; i < gatesOutput.Length; i++)
            {
                gatesOutput[i] = allGatesInputs[i].AndList();
            }

            // result yay!
            return gatesOutput;
        }

        /// <summary>
        ///     A decoder takes inputs like a truth table.
        ///     So using the GenerateCombinations method we can no what inputs need to be negated based on the truth table
        /// </summary>
        /// <param name="combinations"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private IList<bool[]> CreateGatesInputs(IList<bool[]> combinations,
            IList<bool> input)
        {
            var reverseList = input.ReverseList();
            
            var tmp = new bool[combinations.Count][];

            for (var i = 0; i < tmp.Length; i++)
            {
                tmp[i] = GenerateGate(combinations[i], reverseList);
            }

            return tmp;
        }
        
        private bool[] GenerateGate(bool[] combination, IList<bool> input)
        {
            var tmp = new bool[combination.Length];
            
            for (var i = 0; i < tmp.Length; i++)
            {
                tmp[i] = combination[i] ? input[i] : _not.Apply(input[i]);
            }

            return tmp;
        }

        /// <summary>
        ///     Create a truth table of a given length
        ///     e.g. if length == 2 the output would be as following
        ///     | a | b |
        ///     | 0 | 0 |
        ///     | 0 | 1 |
        ///     | 1 | 0 |
        ///     | 1 | 1 |
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private IList<bool[]> GenerateCombinations(int length)
        {
            var numberOfCombinations = (int) Math.Pow(2, length);

            var combinations = new bool[numberOfCombinations][];

            for (uint i = 0; i < combinations.Length; i++)
            {
                combinations[i] = _base10Converter.ToBit(i, length);
            }

            return combinations;
        }
    }
}