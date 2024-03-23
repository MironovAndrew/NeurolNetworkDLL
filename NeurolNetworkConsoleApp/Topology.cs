using System.Runtime.Serialization;

namespace NeurolNetworkConsoleApp
{
    [DataContract]
    public class Topology
    {
        [DataMember]
        public int inputNeuronsCount { get; set; }
        [DataMember]
        public int outputNeuronsCount { get; set; }
        [DataMember]
        public int[] hiddenNeuronsList { get; set; }
        [DataMember]
        public double learningRate { get; set; }



        public Topology(double learningRate, int inputNeuronsCount, int outputNeuronsCount, params int[] hiddenNeuronsList)
        {
            this.inputNeuronsCount = inputNeuronsCount;
            this.outputNeuronsCount = outputNeuronsCount;
            this.hiddenNeuronsList = hiddenNeuronsList;
            this.learningRate = learningRate;
        }

    }
}