
using System.Runtime.Serialization;

namespace NeurolNetworkConsoleApp
{
    [DataContract]
    public class NeurolNetwork
    {
        [DataMember]
        Topology topology;

        [DataMember]
        List<Layer> layerList = new List<Layer>();

        public NeurolNetwork(Topology topology)
        {
            this.topology = topology;


            CreateInputLayer();
            CreateHiddenLayers();
            CreateOutputLayer();
        }






        public double[] Predict(int[] data)
        {
            SendSignalsToInputNeurons(data);
            FeedForwardAfterInputLayer();

            var result = layerList.Last().NeuronList.Select(x => x.Output).ToArray();

            return result;
        }



        void SendSignalsToInputNeurons(int[] inputSignalsList) // 65.4, 54.32, 55.312
        {
            for (int i = 0; i < inputSignalsList.Length; i++)
            {
                Neuron neuron = layerList[0].NeuronList[i];
                neuron.FeedForward(inputSignalsList[i]);
            }
        }




        void FeedForwardAfterInputLayer()
        {
            for (int i = 1; i < layerList.Count; i++)
            {
                Layer currentLayer = layerList[i];
                Layer previousLayer = layerList[i - 1];

                foreach (Neuron neuron in currentLayer.NeuronList)
                {
                    neuron.FeedForward(previousLayer.GetSignals().ToArray());
                }
            }
        }







        public void Train(List<(int[] expected, int[] inputSignals)> dataSet, int epoch)
        {
            for (int i = 0; i < epoch; i++)
            {
                for (int row = 0; row < dataSet.Count; row++)
                {
                    BackPropropagation(dataSet[row].expected, dataSet[row].inputSignals);
                }

                Console.WriteLine($"\t\tЭпоха №{i}");
            }
        }



        public double[] BackPropropagation(int[] expected, int[] inputSignals)
        {
            double[] actuals = Predict(inputSignals);

            double[] errors = new double[expected.Length];

            for (int i = 0; i < errors.Length; i++)
            {
                errors[i] = actuals[i] - expected[i];
            }






            List<Neuron> lastNeuronsList = layerList.Last().NeuronList;

            foreach (Neuron neuron in lastNeuronsList)
            {
                int neuronIndex = lastNeuronsList.IndexOf(neuron);
                neuron.Learn(errors[neuronIndex], topology.learningRate);
            }





            for (int i = layerList.Count - 2; i >= 0; i--)
            {
                Layer layer = layerList[i];
                Layer previousLayer = layerList[i + 1];

                for (int j = 0; j < layer.GetNeuronCount(); j++)
                {
                    for (int k = 0; k < previousLayer.GetNeuronCount(); k++)
                    {
                        Neuron previousNeuron = previousLayer.NeuronList[k];

                        double error = previousNeuron.weightList[j] * previousNeuron.delta;


                        //TODO: чё-то
                        layer.NeuronList[j].Learn(error, topology.learningRate);
                    }
                }
            }


            return errors.Select(x => x * x).ToArray();
        }









        void CreateInputLayer()
        {
            List<Neuron> inputNeuronList = new List<Neuron>(topology.inputNeuronsCount);

            for (int i = 0; i < inputNeuronList.Capacity; i++)
            {
                inputNeuronList.Add(new Neuron(NeuronType.Input, 1));
            }

            layerList.Add(new Layer(inputNeuronList));
        }


        void CreateHiddenLayers()
        {
            List<Neuron> hiddenNeuronList = new List<Neuron>();

            for (int i = 0; i < topology.hiddenNeuronsList.Count(); i++)
            {
                for (int j = 0; j < topology.hiddenNeuronsList[i]; j++)
                {
                    hiddenNeuronList.Add(new Neuron(NeuronType.Hidden, layerList.Last().GetNeuronCount()));
                }

                layerList.Add(new Layer(hiddenNeuronList));

                hiddenNeuronList = new List<Neuron>();
            }

        }


        void CreateOutputLayer()
        {
            List<Neuron> outputNeuronList = new List<Neuron>(topology.outputNeuronsCount);

            for (int i = 0; i < outputNeuronList.Capacity; i++)
            {
                outputNeuronList.Add(new Neuron(NeuronType.Output, layerList.Last().GetNeuronCount()));
            }

            layerList.Add(new Layer(outputNeuronList));
        }
    }
}
