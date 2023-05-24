using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Classes
{
    public class NeuralNetwork
    {
        private List<Layer> layers;
        private double learnRate = 0.2;
        private double h = 0.09;
        private List<TrainingData> trainingDatas;

        public NeuralNetwork(List<int> layers)
        {
            this.layers = new List<Layer>();
            this.trainingDatas = new List<TrainingData>();

            layers.ForEach(n =>
            {
                this.layers.Add(new Layer(n));
            });
            
            Connect();
        }

        public void Connect()
        {
            for(int i = 0; i < this.layers.Count; i++)
            {
                if (i == 0) continue;
                this.layers[i].Connect(this.layers[i - 1]);
            }
        }

        public void AddTrainingData(TrainingData trainingData)
        {
            this.trainingDatas.Add(trainingData);
        }
        public void AddTrainingDatas(List<TrainingData> trainingDatas)
        {
            this.trainingDatas.AddRange(trainingDatas);
        }

        // create an hyperbolic tangent activation function that accept a double value
        public double HyperbolicTangentActivation(double value)
        {
            return Math.Tanh(value);
        }

        // create an SiLU activation function that accept a double value
        public double SiLUActivation(double value)
        {
            return value / (1 + Math.Exp(-value));
        }

        // create a ReLU activation function that accept a double value
        public double ReLUActivation(double value)
        {
            return Math.Max(0, value);
        }

        // create an sigmoid activation function that accept a double value
        public double SigmoidActivation(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

        public double NodeCost(double outputActivation, double expectedOutput)
        {
            double error = outputActivation - expectedOutput;
            return Math.Pow(error, 2);
        }

        public void ForwardProp(TrainingData trainingData)
        {
            for(int i = 0; i < this.layers[0].neurons.Count; i++)
            {
                this.layers[0].neurons[i].value = trainingData.data[i];
            }

            for(int i = 1; i < this.layers.Count; i++)
            {
                for(int j = 0; j < this.layers[i].neurons.Count; j++)
                {
                    this.layers[i].neurons[j].value = HyperbolicTangentActivation(this.layers[i].neurons[j].CalculateOutput());
                }
            }
        }
        

        public double GetSingleCost(TrainingData trainingData)
        {
            ForwardProp(trainingData);
            int i = 0;
            double cost = 0;
            double[] outputData = trainingData.parseTrainingOutput();

            this.layers[layers.Count - 1].neurons.ForEach(neuron =>
            {
                cost += NodeCost(neuron.value, outputData[i++]);
            });

            return cost/ (this.layers.Count + 1);
        }

        public double GetAllCost(List<TrainingData> trainingDatas)
        {
            double cost = 0;
            trainingDatas.ForEach(trainingData =>
            {
                cost += GetSingleCost(trainingData);
            });
            return cost/trainingDatas.Count;
        }

        public double Learn()
        {
            double originalCost = GetAllCost(trainingDatas);

            for(int layerIndex = 1; layerIndex < layers.Count; layerIndex++)
            {
                Layer layer = layers[layerIndex];

                foreach(Neuron neuron in layer.neurons)
                {
                    foreach(WeightedConnection connection in neuron.connections)
                    {
                        connection.weight += h;
                        double deltaCost = GetAllCost(trainingDatas) - originalCost;
                        connection.weight -= h;
                        connection.weightGradient = deltaCost / h;
                    }

                    neuron.bias += h;
                    double deltaBiasCost = GetAllCost(trainingDatas) - originalCost;
                    neuron.bias -= h;
                    neuron.biasGradient = deltaBiasCost / h;
                }
                layer.applyGradient(learnRate);
            }

            return originalCost;
        }

        public double[] Predict(TrainingData data)
        {
            ForwardProp(data);

            double[] output = new double[this.layers[this.layers.Count - 1].neurons.Count];
            for(int i = 0; i < output.Length; i++)
            {
                output[i] = this.layers[this.layers.Count - 1].neurons[i].value;
            }
            return output;
        }
    }
}
