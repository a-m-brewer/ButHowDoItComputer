using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;

namespace ButHowDoItComputer.Parts.Interfaces
{
    public interface IStepper
    {
        StepperOutput Step(bool clk, bool reset);
        StepperOutput Step(bool clk);
    }
}