using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Classes
{
    public class Layer
    {
        public List<Neuron> neurons { get; set; }

        public Layer(int n)
        {
            this.neurons = new List<Neuron>();

            for(int i = 0; i < n; i++)
            {
                  this.neurons.Add(new Neuron(0, 0));
            }
        }

        public void Connect(Layer layer)
        {
            foreach(Neuron neuron in this.neurons)
            {
                foreach(Neuron otherNeuron in layer.neurons)
                {
                    neuron.AddConnection(otherNeuron, new Random().NextDouble() - 0.5);
                }
            }
        }


        

        public void applyGradient(double learnRate)
        {
            foreach(Neuron neuron in this.neurons)
            {
                neuron.bias -= learnRate * neuron.biasGradient;
                neuron.biasGradient = 0;

                foreach(WeightedConnection connection in neuron.connections)
                {
                    connection.weight -= learnRate * connection.weightGradient;
                    connection.weightGradient = 0;
                }
            }
        }
    }
}
