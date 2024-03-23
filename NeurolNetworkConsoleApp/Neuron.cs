using System;
using System.Runtime.Serialization;

namespace NeurolNetworkConsoleApp
{
    [DataContract]
    class Neuron
    {
        [DataMember]
        public List<double> weightList = new List<double>();
        [DataMember]
        public List<double> inputNeuronList = new List<double>();
        [DataMember]
        NeuronType neuronType;
        [DataMember]
        public double WeightAndInputSum { get; private set; }
        [DataMember]
        public double Output { get; private set; } = 0;
        [DataMember]
        public double delta { get; set; } = 0;


        public Neuron(NeuronType neuronType, int inputCount)
        {
            this.neuronType = neuronType;

            SetWeightsRandomly(inputCount);
        }




        void SetWeightsRandomly(int inputCount)
        {
            Random random = new Random();

            for (int i = 0; i < inputCount; i++)
            {
                if (neuronType == NeuronType.Input)
                {
                    weightList.Add(1);
                }
                else
                {
                    weightList.Add(MathF.Round(random.NextSingle(), 6));
                }

            }
        }




        double[] Normalization(double[] inputs)
        {
            double averageInput = 0.0d;
            double error = 0.0d;
            double standardError = 0.0d;





            foreach (var item in inputs)
                averageInput += item;

            averageInput /= inputs.Length;




            foreach (var item in inputs)
                error += Math.Pow((item - averageInput), 2);


            standardError = Math.Sqrt(error / inputs.Length);





            for (int i = 0; i < inputs.Length; i++)
                inputs[i] = (inputs[i] - averageInput) / standardError;


            return inputs;
        }







        public double FeedForward(params double[] inputNeuron)
        {
            if (neuronType != NeuronType.Input)
                inputNeuron = Normalization(inputNeuron);


            inputNeuronList.Clear();
            WeightAndInputSum = 0;

            inputNeuronList.AddRange(inputNeuron);

            for (int i = 0; i < inputNeuron.Length; i++)
            {
                WeightAndInputSum += weightList[i] * inputNeuron[i];
            }








            if (neuronType == NeuronType.Input)
            {
                Output = WeightAndInputSum;
            }
            else
            {
                Output = MathComputer.GetSigmoid(WeightAndInputSum);
            }




            return Output;
        }









        //TODO: М.Б. нужна одна ошибка на нейрон
        public void Learn(double error, double learningRate)
        {
             

            if (neuronType != NeuronType.Input)
            {
                for (int i = 0; i < weightList.Count; i++)
                {
                    //foreach (var error in errors)
                    //{
                    delta = error * MathComputer.GetSigmoid_dx(Output);

                    double newWeight = weightList[i] - inputNeuronList[i] * delta * learningRate;

                    weightList[i] = newWeight;
                    // }
                }
            }

        }





        public override string ToString()
        {
            return $"output: {Output}\n" +
                $"Sum: {WeightAndInputSum}\n" +
                $"Delta: {delta}\n" +
                $"{new string('-', 10)}";
        }
    }
}
