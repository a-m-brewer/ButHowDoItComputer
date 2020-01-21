using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IStepper
    {
        StepperOutput Step(IBit clk, IBit reset);
        StepperOutput Step(IBit clk);
    }
}