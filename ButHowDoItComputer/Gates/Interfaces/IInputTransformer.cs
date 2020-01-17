using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IInputTransformer<T>
    {
        IEnumerable<T> Apply(params T[] inputs);
    }
}