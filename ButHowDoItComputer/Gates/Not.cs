namespace ButHowDoItComputer.Gates
{
    public class Not : ISingleInputGate
    {
        public IBit Apply(IBit bit)
        {
            return new Bit(!bit.State);
        }
    }
}