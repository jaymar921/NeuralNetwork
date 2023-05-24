using NeuralNetworks.Classes;
using System.Diagnostics;

namespace NeuralNetworks
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            NeuralNetwork neural = new NeuralNetwork(new List<int> { 4, 16 });
            List<TrainingData> trainingData = new List<TrainingData>();
            initializeTrainingData(trainingData);

            neural.AddTrainingDatas( trainingData);

            int n = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            while((n++) <= 10000)
            {
                Console.WriteLine("Cost: " + neural.Learn());
                //neural.Learn();
            }

            stopwatch.Stop();
            Console.WriteLine("Time: " + stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Trained at Cost: " + neural.Learn());
            while (true)
            {
                Console.Write("Input: ");
                string? input = Console.ReadLine();
                if (input == null) continue;
                char[] arr = input.ToCharArray();
                double[] arr2 = new double[arr.Length];
                for(int i = 0; i < arr.Length; i++)
                {
                    arr2[i] = double.Parse(arr[i].ToString());
                }
                TrainingData trainingData1 = new TrainingData(arr2, 0);
                string prediction = "Prediction: ";
                int z = 0;
                for(int i = 0; i < neural.Predict(trainingData1).Length; i++)
                {
                    if (neural.Predict(trainingData1)[i] > z)
                    {
                        z = i;
                    }
                }
                prediction += z;
                Console.WriteLine(prediction);
            }
        }

        static void initializeTrainingData(List<TrainingData> trainingData)
        {
            trainingData.Add(new TrainingData(new double[] { 0, 0, 0, 0 }, 0));
            trainingData.Add(new TrainingData(new double[] { 0, 0, 0, 1 }, 1));
            trainingData.Add(new TrainingData(new double[] { 0, 0, 1, 0 }, 2));
            trainingData.Add(new TrainingData(new double[] { 0, 0, 1, 1 }, 3));
            trainingData.Add(new TrainingData(new double[] { 0, 1, 0, 0 }, 4));
            trainingData.Add(new TrainingData(new double[] { 0, 1, 0, 1 }, 5));
            trainingData.Add(new TrainingData(new double[] { 0, 1, 1, 0 }, 6));
            trainingData.Add(new TrainingData(new double[] { 0, 1, 1, 1 }, 7));
            trainingData.Add(new TrainingData(new double[] { 1, 0, 0, 0 }, 8));
            trainingData.Add(new TrainingData(new double[] { 1, 0, 0, 1 }, 9));
            trainingData.Add(new TrainingData(new double[] { 1, 0, 1, 0 }, 10));
            trainingData.Add(new TrainingData(new double[] { 1, 0, 1, 1 }, 11));
            trainingData.Add(new TrainingData(new double[] { 1, 1, 0, 0 }, 12));
            trainingData.Add(new TrainingData(new double[] { 1, 1, 0, 1 }, 13));
            trainingData.Add(new TrainingData(new double[] { 1, 1, 1, 0 }, 14));
            trainingData.Add(new TrainingData(new double[] { 1, 1, 1, 1 }, 15));
        }
    }
}