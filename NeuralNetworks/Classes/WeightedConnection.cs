using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Classes
{
    public class WeightedConnection
    {

        public Neuron neuron { get; set; }
        public double weight { get; set; }
        public double weightGradient { get; set; }

        public WeightedConnection(Neuron neuron, double weight)
        {
            this.neuron = neuron;
            this.weight = weight;
            this.weightGradient = 0;
        }

    }
}
