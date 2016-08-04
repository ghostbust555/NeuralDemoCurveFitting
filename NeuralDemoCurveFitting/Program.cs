using DocumentClasssifier.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralDemoCurveFitting
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var networkGraph = new NetworkGraph();

            new Thread(() =>
            {
                var nn = new NeuralNetwork();
                nn.CreateNetwork();
                nn.TrainNetwork(networkGraph);
            }).Start();

            Application.EnableVisualStyles();
           // Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(networkGraph);
        }
    }
}
