using System.Collections.Generic;

namespace ButHowDoItComputer.Gates.Interfaces
{
    public interface IInputTransformer<T>
    {
        IList<T> ApplyParams(params T[] inputs);
        IList<T> Apply(IList<T> inputs);
    }
}