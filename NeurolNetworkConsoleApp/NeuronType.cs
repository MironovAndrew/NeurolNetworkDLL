using System.Runtime.Serialization;

namespace NeurolNetworkConsoleApp
{
    [DataContract]
    public enum NeuronType
    {
        Input,
        Hidden,
        Output
    }

}
