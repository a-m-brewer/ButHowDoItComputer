using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;
using ButHowDoItComputer.Utils;

namespace ButHowDoItComputer.Gates
{
    public class Decoder : IDecoder
    {
        private readonly INot _not;
        private readonly IAnd _and;

        public Decoder(INot not, IAnd and)
        {
            _not = not;
            _and = and;
        }

        public IEnumerable<bool> Apply(params bool[] inputs)
        {
            var inputList = inputs.ToList();
            // get a truth table based on the length of the input
            var combinations = GenerateCombinations(inputList.Count).ToArray();
            
            // apply the not inputs based on each truth table row
            var allGatesInputs = CreateGatesInputs(combinations, inputList).ToArray();

            // take the inputs and apply and to them
            var gatesOutput= allGatesInputs.Select(s => _and.Apply(s.ToArray())).ToList();

            // result yay!
            return gatesOutput;
        }

        /// <summary>
        /// A decoder takes inputs like a truth table.
        /// So using the GenerateCombinations method we can no what inputs need to be negated based on the truth table
        /// </summary>
        /// <param name="combinations"></param>
        /// <param name="inputList"></param>
        /// <returns></returns>
        private IEnumerable<List<bool>> CreateGatesInputs(IEnumerable<BitList> combinations, IEnumerable<bool> inputList)
        {
            var input = inputList.Reverse().ToList();
            return combinations.Select(
                combination => 
                    combination.Select(
                        (bit, bitIndex) => 
                            bit 
                                ? input[bitIndex] 
                                : _not.Apply(input[bitIndex])).ToList()
                    )
                .ToList();
        }

        /// <summary>
        /// Create a truth table of a given length
        /// e.g. if length == 2 the output would be as following
        /// | a | b |
        /// | 0 | 0 |
        /// | 0 | 1 |
        /// | 1 | 0 |
        /// | 1 | 1 |
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private IEnumerable<BitList> GenerateCombinations(int length)
        {
            var output = new List<BitList>();
            var numberOfCombinations = (int) Math.Pow(2, length);
            for (var bitList = new BitList(); bitList < numberOfCombinations; bitList++)
            {
                if (bitList.Count != length)
                {
                    var paddingNeeded = length - bitList.Count;
                    bitList.AddRange(paddingNeeded.BitListOfLength());
                }

                output.Add(bitList);
            }

            return output;
        }
    }
}