using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Classes
{
    public class TrainingData
    {
        public double[] data { get; set; }
        public double value { get; set; } = 0;
        
        public TrainingData(double[] data, double value)
        {
            this.data = data;
            this.value = value;
        }

        public double[] parseTrainingOutput(TrainingData trainingData)
        {
            double[] arr = new double[16];
            for(int i = 0; i < arr.Length; i++)
            {
                if(i == trainingData.value)
                {
                    arr[i] = 1;
                }
                else
                {
                    arr[i] = -1;
                }
            }
            return arr;
        }

        public double[] parseTrainingOutput()
        {
            double[] arr = new double[16];
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == this.value)
                {
                    arr[i] = 1;
                }
                else
                {
                    arr[i] = -1;
                }
            }
            return arr;
        }
    }
}
