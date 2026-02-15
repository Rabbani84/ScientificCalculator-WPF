using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;
using System.Diagnostics;

namespace Rechner
{
    /// <summary>
    /// Interaktionslogik für Gardanische_Gleichung.xaml
    /// </summary>
    public partial class Gardanische_Gleichung : Page
    {
        MainWindow min3;
        public Gardanische_Gleichung(MainWindow min3)
        {
            InitializeComponent();
            this.min3 = min3;
        }

        private void eintragen_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                min3.ergebnis.Text=string.Empty;
                int a = ersteZ.Value ?? 0;
                int b = zweiZ.Value ?? 0;
                int c = dreiZ.Value ?? 0;
                int d = vierZ.Value ?? 0;

                double root1;
                double root2;
                double root3;

                double q = (3 * a * c - b * b) / (9 * a * a);
                double p = (3 * a * d - b * c) / (6 * a * a);

                double delta = Math.Pow(q, 3) + Math.Pow(p, 2);

                if (delta > 0)
                {

                    double sqrtDelta = Math.Sqrt(delta);
                    double alpha = Math.Pow(-q / 2 + sqrtDelta, 1.0 / 3);
                    double beta = Math.Pow(-q / 2 - sqrtDelta, 1.0 / 3);


                    root1 = -b / (3 * a) - (alpha + beta) / 2;
                    root2 = -b / (3 * a) + (alpha + beta) / 2;
                    root3 = Convert.ToDouble(-b / (3 * a) + Complex.ImaginaryOne * (alpha - beta) / 2);

                    
                    
                    
                }
                else if (delta == 0)
                {
                    
                    double alpha = Math.Pow(-q / 2, 1.0 / 3);

                    
                     root1 = -b / (3 * a) - alpha;
                     root2 = -b / (3 * a) + (alpha / 2);
                     root3 = -b / (3 * a) - (alpha / 2);

                    
                    
                    
                }
                else
                {
                    
                    double theta = Math.Acos(-p / Math.Sqrt(-Math.Pow(q, 3)));

                    
                     root1 = 2 * Math.Sqrt(-q / 3) * Math.Cos(theta / 3) - b / (3 * a);
                     root2 = 2 * Math.Sqrt(-q / 3) * Math.Cos((theta + 2 * Math.PI) / 3) - b / (3 * a);
                     root3 = 2 * Math.Sqrt(-q / 3) * Math.Cos((theta + 4 * Math.PI) / 3) - b / (3 * a);

                    
                    
                    
                }
            
                var model = new PlotModel { Title = $"y=({a})X³+({b})X²+({c})X+({d})" };
                var series = new LineSeries { Title = $"y=({a})X³+({b})X²+({c})X+({d})" };


                model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -40, Maximum = 40 });
                model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -10, Maximum = 10 });

                min3.tb.Text = $"({a})X³+({b})X²+({c})X+({d})=0";
                min3.lb.Items.Add(min3.tb.Text);
                for (double x = -15; x <= 15; x += 0.001)
                {
                    double y = a * x * x * x + b * x * x + c * x + d;
                    if(Convert.ToInt16(y) == 0)
                    {
                        
                        min3.lb.Items.Add($" X = {x} ");
                        Debug.WriteLine($" X = {x} ");
                    }
                    series.Points.Add(new DataPoint(x, y));
                }

               
                min3.ergebnis.Text = $"Lösung nach X1 = {root1},X2 = {root2},X3 = {root3} ";
                min3.lb.Items.Add(min3.tb.Text + "\n" + min3.ergebnis.Text);

                model.Series.Add(series);
                plotView.Model = model;

            }
            catch
            {
                min3.ergebnis.Text = "Error  !!!";
            }
        }
    }
}
