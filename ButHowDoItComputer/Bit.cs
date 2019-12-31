namespace ButHowDoItComputer
{
    public class Bit : IBit
    {
        public Bit(bool initialState)
        {
            State = initialState;
        }
        
        public bool State { get; set; }
    }
}