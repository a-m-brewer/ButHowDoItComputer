using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Utils;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Decoder : IDecoder
    {
        private readonly IAnd _and;
        private readonly IBase10Converter _base10Converter;
        private readonly INot _not;

        public Decoder(INot not, IAnd and, IBase10Converter base10Converter)
        {
            _not = not;
            _and = and;
            _base10Converter = base10Converter;
        }

        public IEnumerable<bool> Apply(params bool[] inputs)
        {
            // get a truth table based on the length of the input
            var combinations = GenerateCombinations(inputs.Length);

            // apply the not inputs based on each truth table row
            var allGatesInputs = CreateGatesInputs(combinations, inputs);

            // take the inputs and apply and to them
            var gatesOutput = allGatesInputs.Select(s => _and.Apply(s));

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
        private IEnumerable<bool[]> CreateGatesInputs(IEnumerable<BitList> combinations,
            bool[] input)
        {
            Array.Reverse(input);
            
            return combinations.Select(
                    combination => GenerateGate(combination, input)
            );
        }
        
        private bool[] GenerateGate(BitList combination, IReadOnlyList<bool> input)
        {
            var tmp = new bool[combination.Count];
            
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
        private IEnumerable<BitList> GenerateCombinations(int length)
        {
            var numberOfCombinations = (int) Math.Pow(2, length);

            var combinations = new BitList[numberOfCombinations];

            for (uint i = 0; i < combinations.Length; i++)
            {
                var bits = _base10Converter.ToBit(i, length);
                combinations[i] = new BitList(bits);
            }

            return combinations;
        }
    }
}