using System.Runtime.Serialization;

namespace NeurolNetworkConsoleApp
{
    [DataContract]
    class Layer
    {
        [DataMember]
        public List<Neuron> NeuronList = new List<Neuron>();

        public Layer(List<Neuron> neuronList)
        {
            this.NeuronList = neuronList;
        }


        public List<double> GetSignals()
        {
            return NeuronList.Select(x => x.Output).ToList();
        }

        public int GetNeuronCount()
        {
            return NeuronList.Count;
        }
    }
}
