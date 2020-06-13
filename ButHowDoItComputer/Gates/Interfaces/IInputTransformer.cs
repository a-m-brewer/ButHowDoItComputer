using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IInputTransformer<T>
    {
        IList<T> Apply(params T[] inputs);
    }
}