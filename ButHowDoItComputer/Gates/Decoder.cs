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
        private readonly IBitFactory _bitFactory;

        public Decoder(INot not, IAnd and, IBitFactory bitFactory)
        {
            _not = not;
            _and = and;
            _bitFactory = bitFactory;
        }

        public IEnumerable<IBit> Apply(IEnumerable<IBit> inputs)
        {
            var inputList = inputs.ToList();
            // get a truth table based on the length of the input
            var combinations = GenerateCombinations(inputList.Count);
            
            // apply the not inputs based on each truth table row
            var allGatesInputs = CreateGatesInputs(combinations, inputList);

            // take the inputs and apply and to them
            var gatesOutput= allGatesInputs.Select(s => _and.Apply(s)).ToList();

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
        private IEnumerable<List<IBit>> CreateGatesInputs(IEnumerable<BitList> combinations, IEnumerable<IBit> inputList)
        {
            var input = inputList.Reverse().ToList();
            return combinations.Select(
                combination => 
                    combination.Select(
                        (bit, bitIndex) => 
                            bit.State 
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
                    bitList.AddRange(_bitFactory.Create(length - bitList.Count));
                }

                output.Add(bitList);
            }

            return output;
        }
    }
}