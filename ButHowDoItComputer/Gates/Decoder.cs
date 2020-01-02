using System;
using System.Collections.Generic;
using System.Linq;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Gates.Interfaces;

namespace ButHowDoItComputer.Gates
{
    public class Decoder
    {
        private readonly INot _not;
        private readonly IAnd _and;

        public Decoder(INot not, IAnd and)
        {
            _not = not;
            _and = and;
        }
        
        public IList<IBit> Apply(IList<IBit> inputs)
        {
            var combinations = Cominations(inputs.Count);

            var output = new List<List<IBit>>();
            for (int i = 0; i < combinations.Count; i++)
            {
                var tempList = new List<IBit>();
                for (int j = 0; j < combinations[i].Count; j++)
                {
                    if (combinations[i][j])
                    {
                        tempList.Add(inputs[j]); 
                    }
                    else
                    {
                        tempList.Add(_not.Apply(inputs[j]));
                    }
                }
                output.Add(tempList);
            }
            
            var output2 = output.Select(s => _and.Apply(s));

            return output2.ToList();
        }

        public List<List<bool>> Cominations(int n)
        {
            var matrix = new List<List<bool>>();

            var count = Math.Pow(2, n);
            for (var i = 0; i < count; i++)
            {
                var str = Convert.ToString(i, 2).PadLeft(n, '0');
                var boolArr = str.Select((x) => x == '1').ToList();
                
                matrix.Add(boolArr);
            }

            return matrix;
        }
    }
}