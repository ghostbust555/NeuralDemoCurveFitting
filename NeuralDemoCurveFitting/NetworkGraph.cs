using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot.WindowsForms;
using OxyPlot.Series;
using OxyPlot.Axes;
using NeuralDemoCurveFitting;

namespace DocumentClasssifier.Neural
{
    public partial class NetworkGraph : Form
    {
        PlotView Plot;
        public int CX = 0;
        public int Offset = 1;
        public LineSeries LearnedFunction;
        public LineSeries Function;
        public LinearAxis xAxis;
        public LinearAxis yAxis;

        public NetworkGraph()
        {
            InitializeComponent();

            Plot = new PlotView();
            Plot.Model = new PlotModel();
            Plot.Dock = DockStyle.Fill;
            this.Controls.Add(Plot);

            Plot.Model.PlotType = PlotType.XY;
            Plot.Model.Background = OxyColor.FromRgb(255, 255, 255);
            Plot.Model.TextColor = OxyColor.FromRgb(0, 0, 0);

            // Create Line series
            Function = new LineSeries { Title = "Sin(x)", StrokeThickness = 1 };
            LearnedFunction = new LineSeries { Title = "NN(x)", StrokeThickness = 1 };

            // add Series and Axis to plot model
            Plot.Model.Series.Add(Function);
            Plot.Model.Series.Add(LearnedFunction);
            
            xAxis = new LinearAxis(AxisPosition.Bottom, 0.0, NeuralNetwork.MaxX);
            Plot.Model.Axes.Add(xAxis);
            yAxis = new LinearAxis(AxisPosition.Left, -1, 1);
            Plot.Model.Axes.Add(yAxis);
        }

        public void AddPoint(double x, double actualValue, double trainedValue)
        {
            lock (Function){
                lock (LearnedFunction)
                {
                    Function.Points.Add(new DataPoint(x, actualValue));
                    LearnedFunction.Points.Add(new DataPoint(x, trainedValue));
                }
            }
        }

        public void Update()
        {
            Plot.Invalidate();
        }

        public void AddTitle(string title)
        {
            Plot.Model.Title = title;
        }

        public void ResetData()
        {
            Function.Points.Clear();
            LearnedFunction.Points.Clear();
            Plot.Model.ResetAllAxes();
            Plot.Invalidate();
        }
    }
}
