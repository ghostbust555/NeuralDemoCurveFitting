using Accord.Neuro;
using AForge.Neuro;
using AForge.Neuro.Learning;
using DocumentClasssifier.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuralDemoCurveFitting
{
    public class NeuralNetwork
    {
        public double [][] InputData;
        public double [][] OutputData;

        ActivationNetwork network;
        BackPropagationLearning teacher;

        public const double MaxX = 2 * Math.PI;
        public const double Step = Math.PI / 10;

        public double[][] GetSinWavePoints()
        {
            var listOfPoints = new List<double[]>();

            for(double x = 0; x < MaxX; x += Step)
            {
                listOfPoints.Add(new double[] { Math.Sin(x)});
            }

            return listOfPoints.ToArray();
        }

        public double[][] GetXvals()
        {
            var listOfPoints = new List<double[]>();

            for (double x = 0; x < MaxX; x += Step)
            {
                listOfPoints.Add(new double[] { x });
            }

            return listOfPoints.ToArray();
        }

        public void CreateNetwork()
        {            

            // create neural network
            network = new ActivationNetwork(
                new BipolarSigmoidFunction(),
                1, // inputs in the network
                10, // neurons in the first (hidden) layer
                1); // neuron in the second (output) layer    
            
        }

        public void TrainNetwork(NetworkGraph graph)
        {
            teacher = new BackPropagationLearning(network);

            InputData = GetXvals();
            OutputData = GetSinWavePoints();

            double error = int.MaxValue;

            int iteration = 1;
            while (error > .001)
            {
                error = teacher.RunEpoch(InputData, OutputData);

                graph.AddTitle(string.Format("SIN(x) Iteration {0}-{1:0.00} Error", iteration, error));
                graph.ResetData();

                int i = 0;
                foreach (var x in InputData)
                {
                    graph.AddPoint(x[0], OutputData[i][0], network.Compute(x)[0]);
                    i++;
                }

                graph.Update();

                Thread.Sleep((int)Math.Max(error, 10));
                
                iteration++;

            }            

            var asdf = 0;    
        }
    }
}
