using ButHowDoItComputer.DataTypes;
using ButHowDoItComputer.DataTypes.Interfaces;
using ButHowDoItComputer.Parts.Interfaces;
using ButHowDoItComputer.Utils.Interfaces;

namespace ButHowDoItComputer.Components
{
    public class RegisterCaez : ICpuSettableSubscriber
    {
        private readonly Caez _caez;

        public RegisterCaez(Caez caez)
        {
            _caez = caez;
        }

        public void Apply()
        {
            
        }

        public IBit Set { get; set; }
    }
}