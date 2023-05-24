using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Classes
{
    public class Neuron
    {
        public double bias;
        public double biasGradient;
        public double value;
        public List<WeightedConnection> connections;

        public Neuron(double value, double bias)
        {
            this.bias = bias;
            this.value = value;
            this.connections = new List<WeightedConnection>();
            this.biasGradient = 0;
        }

        public double CalculateOutput()
        {
            double value = this.bias;
            foreach(WeightedConnection connection in this.connections)
            {
                value += connection.weight * connection.neuron.value;
            }

            return value;
        }

        public void AddConnection(Neuron neuron, double weight)
        {
            this.connections.Add(new WeightedConnection(neuron, weight));
        }

    }
}
